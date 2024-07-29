namespace JMF_Web_Forum_API.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
