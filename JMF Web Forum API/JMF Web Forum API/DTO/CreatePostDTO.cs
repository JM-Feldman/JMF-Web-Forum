namespace JMF_Web_Forum_API.DTO
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Tags { get; set; }
    }
}
