using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyStagram.Infrastructure.Database;
using MyStagram.Core.Filters;
using MyStagram.Core.Helpers;
using MyStagram.Core.Services;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.SignalR;
using MyStagram.Core.Settings;
using MyStagram.Core.Validators;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Data;
using MyStagram.API.BackgroundServices;
using MyStagram.Core.Data.Mongo;
using MyStagram.Infrastructure.Database.Mongo;
using MyStagram.Infrastructure.Logging;
using MyStagram.Core.Logging;
using Microsoft.Extensions.Options;
using MyStagram.API.BackgroundServices.Interfaces;
using MyStagram.Infrastructure.Email;
using MyStagram.Infrastructure.Upload;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddDefaultTokenProviders();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                opt.UseNpgsql(Configuration.GetConnectionString(AppSettingsKeys.ConnectionString), b => b.MigrationsAssembly("MyStagram.API"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection(AppSettingsKeys.Token).Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddMvc(options =>
           {
               var policy = new AuthorizationPolicyBuilder()
                   .RequireAuthenticatedUser()
                   .Build();
               options.Filters.Add(new AuthorizeFilter(policy));
               options.Filters.Add(new ExceptionFilter());
               options.Filters.Add(new BlockFilter());
               options.Filters.Add(new MainValidator());

           })
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .AddMvcOptions(options => options.EnableEndpointRouting = false);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AdminPolicy, policy => policy.RequireRole(Constants.AdminRoles));
                options.AddPolicy(Constants.HeadAdminPolicy, policy => policy.RequireRole(Constants.HeadAdminRole));
            });
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddOptions();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            services.AddCors(options => options.AddPolicy(Constants.CorsPolicy, build =>
            {
                build.AllowCredentials()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithOrigins("http://localhost:4200");
            }));

            services.AddMediatR(Assembly.Load("MyStagram.Core"));

            #region services

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddSingleton<IFilesManager, FilesService>();
            services.AddSingleton<IReadOnlyFilesService, FilesService>();
            services.AddSingleton<INLogger, NLogger>();

            services.AddScoped<IDatabase, Database>();
            services.AddScoped<ILogManager, LogManager>();
            services.AddScoped<IDatabaseManager, DatabaseManager>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddScoped<IConnectionManager, ConnectionManager>();
            services.AddScoped<IHubManager, HubManager>();

            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IReadOnlyLogManager, LogManager>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReadOnlyAuthService, AuthService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IReadOnlyRolesService, RolesService>();
            services.AddScoped<IMainService, MainService>();
            services.AddScoped<IReadOnlyMainService, MainService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IReadOnlyProfileService, ProfileService>();
            services.AddScoped<IFollowersService, FollowersService>();
            services.AddScoped<IReadOnlyFollowersService, FollowersService>();
            services.AddScoped<IMessenger, Messenger>();
            services.AddScoped<IReadOnlyMessenger, Messenger>();
            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<IReadOnlyStoryService, StoryService>();

            #endregion

            #region settings

            services.Configure<EmailSettings>(Configuration.GetSection(nameof(EmailSettings)));

            services.Configure<MongoDatabaseSettings>(Configuration.GetSection(nameof(MongoDatabaseSettings)));

            services.AddSingleton<IMongoDatabaseSettings>(settings => settings.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            #endregion

            services.AddHostedService<ServerHostedService>();
            services.AddAutoMapper(typeof(MapperProfiles));
            services.AddDataProtection();
            services.AddDirectoryBrowser();
            services.AddSignalR();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(Constants.CorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<HubClient>("/api/hub");
            });

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, @"files")),
                RequestPath = new PathString("/files"),
                EnableDirectoryBrowsing = true
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            StorageLocation.Init(Configuration.GetValue<string>(AppSettingsKeys.ServerAddress));
        }
    }
}
