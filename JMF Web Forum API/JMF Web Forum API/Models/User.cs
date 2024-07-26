using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JMF_Web_Forum_API.Models
 
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Userrole { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
    }
}
