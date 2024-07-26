namespace JMF_Web_Forum_API.DTO
{
    public class RemoveTagsDTO
    {
        public int PostId { get; set; }
        public List<string> Tags { get; set; }
    }
}
