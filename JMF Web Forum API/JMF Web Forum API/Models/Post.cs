namespace JMF_Web_Forum_API.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }

    }
}
