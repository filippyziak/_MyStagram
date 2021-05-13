using System.Linq;
using AutoMapper;
using MyStagram.Core.Logic.Requests.Command.Main;
using MyStagram.Core.Logic.Requests.Command.Profile;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Models.Dtos.Auth;
using MyStagram.Core.Models.Dtos.Main;
using MyStagram.Core.Models.Dtos.Messenger;
using MyStagram.Core.Models.Dtos.Profile;
using MyStagram.Core.Models.Dtos.Social;
using MyStagram.Core.Models.Dtos.Story;

namespace MyStagram.Core.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserAuthDto>();
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<User, UserProfileDto>()
            .ForMember(dest => dest.FollowersCount, opt => opt.MapFrom(u => u.Followers.Where(f => f.RecipientAccepted).Count()))
            .ForMember(dest => dest.FollowingCount, opt => opt.MapFrom(u => u.Following.Where(f => f.RecipientAccepted).Count()))
            .ForMember(dest => dest.FollowersRequests, opt => opt.MapFrom(u => u.Followers.Where(f => !f.RecipientAccepted)))
            .ForMember(dest => dest.FollowingRequests, opt => opt.MapFrom(u => u.Following.Where(f => !f.RecipientAccepted)))
            .ForMember(dest => dest.Followers, opt => opt.MapFrom(u => u.Followers.Where(f => f.RecipientAccepted)))
            .ForMember(dest => dest.Following, opt => opt.MapFrom(u => u.Following.Where(f => f.RecipientAccepted)));
            CreateMap<User, SearchUserDto>();
            CreateMap<Post, PostDto>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(p => p.User.UserName))
                    .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(p => p.Comments.Count))
                    .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(p => p.Likes.Count));
            CreateMap<Post, PostsDto>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(p => p.User.UserName))
                    .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(p => p.Likes.Count));
            CreateMap<CreatePostRequest, Post>()
            .ForMember(dest => dest.Created, opt => opt.Ignore());
            CreateMap<UpdatePostRequest, Post>()
            .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(c => c.User.UserName))
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(c => StorageLocation.BuildLocation(c.User.PhotoUrl)));

            CreateMap<Like, LikeDto>();

            CreateMap<Follower, FollowerDto>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(f => f.Sender.UserName))
            .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(f => StorageLocation.BuildLocation(f.Sender.PhotoUrl)))
            .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(f => f.Recipient.UserName))
            .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(f => StorageLocation.BuildLocation(f.Recipient.PhotoUrl)));

            CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(m => m.Sender.UserName))
            .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(m => StorageLocation.BuildLocation(m.Sender.PhotoUrl)))
            .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(m => m.Recipient.UserName))
            .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(m => StorageLocation.BuildLocation(m.Recipient.PhotoUrl)));

            CreateMap<User, RecipientDto>();

            CreateMap<Story, StoryDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(s => s.User.UserName))
            .ForMember(dest => dest.UserPhotoUrl, opt => opt.MapFrom(s => StorageLocation.BuildLocation(s.User.PhotoUrl)))
            .ForMember(dest => dest.WatchedByCount, opt => opt.MapFrom(s => s.UserStories.Count));

            CreateMap<UserStory, UserStoryDto>();
        }
    }
}