namespace JMF_Web_Forum_API.DTO
{
    public class PostLikeDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool IsLike { get; set; } // true for like, false for dislike
    }
}
