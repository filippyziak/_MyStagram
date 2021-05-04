using System;
using System.Collections.Generic;

namespace MyStagram.Core.Models.Dtos.Main
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string PhotoUrl { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhotoUrl { get; set; }
        public int CommentsCount { get; set; }
        public int LikesCount { get; set; }

        public ICollection<LikeDto> Likes { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}