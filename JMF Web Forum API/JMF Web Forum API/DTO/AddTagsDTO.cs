namespace JMF_Web_Forum_API.DTO
{
    public class AddTagsDTO
    {
        public int PostId { get; set; }
        public List<string> Tags { get; set; }
    }
}
