using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Core.Models.Domain.Auth
{
    public class User : IdentityUser<string>
    {
        public override string Id { get; set; } = Utils.Id();
        public DateTime DateOfBirth { get; protected set; } = DateTime.Now;
        public bool IsPrivate { get; protected set; } = default(bool);
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Country { get; protected set; }
        public string City { get; protected set; }
        public string Description { get; protected set; }
        public string PhotoUrl { get; protected set; }
        public bool IsBlocked { get; protected set; } = default(bool);
        public DateTime LastActive { get; protected set; } = DateTime.Now;
        public DateTime Created { get; protected set; } = DateTime.Now;

        public virtual ICollection<UserRole> UserRoles { get; protected set; } = new HashSet<UserRole>();
        public virtual ICollection<Post> Posts { get; protected set; } = new HashSet<Post>();
        public virtual ICollection<Comment> Comments { get; protected set; } = new HashSet<Comment>();
        public virtual ICollection<Like> Likes { get; protected set; } = new HashSet<Like>();
        public virtual ICollection<Follower> Followers { get; protected set; } = new HashSet<Follower>();
        public virtual ICollection<Follower> Following { get; protected set; } = new HashSet<Follower>();
        public virtual ICollection<Message> MessagesSent { get; protected set; } = new HashSet<Message>();
        public virtual ICollection<Message> MessagesReceived { get; protected set; } = new HashSet<Message>();
        public virtual ICollection<Connection.Connection> Connections { get; protected set; } = new HashSet<Connection.Connection>();
        public virtual ICollection<Core.Models.Domain.Social.Story> Stories { get; protected set; } = new HashSet<Core.Models.Domain.Social.Story>();
        public virtual ICollection<UserStory> UserStories { get; protected set; } = new HashSet<UserStory>();


        public static User Create(string email, string userName) => new User
        {
            Email = email,
            UserName = userName
        };

        public void UpdateProfile(string userName, string surname, string name, string Description, string email)
        {
            this.UserName = userName.ToLower();
            this.NormalizedUserName = userName.ToUpper();
            this.Surname = surname;
            this.Name = name;
            this.Description = Description;
            this.Email = email.ToLower();
            this.NormalizedEmail = email.ToUpper();
        }

        public void SetAvatar(string photoUrl)
        {
            this.PhotoUrl = photoUrl;
        }

        public void ChangePrivacy(bool isPrivate)
        {
            this.IsPrivate = isPrivate;
        }

        public void BlockUser()
        {
            this.IsBlocked = !IsBlocked;
        }
    }
}