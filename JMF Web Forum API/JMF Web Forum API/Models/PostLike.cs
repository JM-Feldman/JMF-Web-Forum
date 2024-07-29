namespace JMF_Web_Forum_API.Models
{
    public class PostLike
    {
        public int LikeId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsLike { get; set; } // true for like, false for dislike
    }
}
