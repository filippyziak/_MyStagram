using System;
using System.Collections.Generic;
using MyStagram.Core.Models.Dtos.Main;
using MyStagram.Core.Models.Dtos.Social;

namespace MyStagram.Core.Models.Dtos.Profile
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhotoUrl { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }



        public ICollection<PostDto> Posts { get; set; }
        public ICollection<FollowerDto> Followers { get; set; }
        public ICollection<FollowerDto> Following { get; set; }
        public ICollection<FollowerDto> FollowersRequests { get; set; }
        public ICollection<FollowerDto> FollowingRequests { get; set; }
        
    }
}