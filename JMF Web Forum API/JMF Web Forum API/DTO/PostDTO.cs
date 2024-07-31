namespace JMF_Web_Forum_API.DTO
{
  public class PostDTO
  {
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DatePosted { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<string> Tags { get; set; }
    public List<CommentDTO> Comments { get; set; }
  }
}
