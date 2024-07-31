using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using JMF_Web_Forum_API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JMF_Web_Forum_API.Models;
using JMF_Web_Forum_API.DTO;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
  private readonly AppDbContext _context;

  public PostsController(AppDbContext context)
  {
    _context = context;
  }

  //Get posts by the latest date
  [HttpGet("latest")]
  public async Task<ActionResult<IEnumerable<Post>>> GetPostsByLatestDate()
  {
    var posts = await _context.Posts
     .Include(p => p.User)
     .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
     .Include(p => p.Comments).ThenInclude(c => c.User)
     .OrderByDescending(p => p.DatePosted)
     .Select(p => new PostDTO
     {
       PostId = p.PostId,
       Title = p.Title,
       Content = p.Content,
       DatePosted = p.DatePosted,
       UserName = p.User.Username,
       Tags = p.PostTags.Select(pt => pt.Tag.Name).ToList(),
       Comments = p.Comments.Select(c => new CommentDTO
       {
         CommentId = c.CommentId,
         Content = c.Content,
         DatePosted = c.DatePosted,
         UserId = c.User.UserId,
         UserName = c.User.Username
       }).ToList()
     })
     .ToListAsync();

    return Ok(posts);
  }

  //Search posts by tags
  [HttpGet("search/byTag")]
  public async Task<ActionResult<IEnumerable<PostDTO>>> SearchPostsByTags([FromQuery] List<string> tags)
  {
    var posts = await _context.Posts
        .Include(p => p.User)
        .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
        .Include(p => p.Comments).ThenInclude(c => c.User)
        .Where(p => p.PostTags.Any(pt => tags.Contains(pt.Tag.Name)))
        .Select(p => new PostDTO
        {
          PostId = p.PostId,
          Title = p.Title,
          Content = p.Content,
          DatePosted = p.DatePosted,
          UserName = p.User.Username,
          Tags = p.PostTags.Select(pt => pt.Tag.Name).ToList(),
          Comments = p.Comments.Select(c => new CommentDTO
          {
            CommentId = c.CommentId,
            Content = c.Content,
            DatePosted = c.DatePosted,
            UserId = c.User.UserId,
            UserName = c.User.Username
          }).ToList()
        })
        .ToListAsync();

    return Ok(posts);
  }

  //Search posts by author
  [HttpGet("search/byAuthor")]
  public async Task<ActionResult<IEnumerable<PostDTO>>> SearchPostsByAuthor([FromQuery] string author)
  {
    var posts = await _context.Posts
        .Include(p => p.User)
        .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
        .Include(p => p.Comments).ThenInclude(c => c.User)
        .Where(p => p.User.Username.Contains(author))
        .Select(p => new PostDTO
        {
          PostId = p.PostId,
          Title = p.Title,
          Content = p.Content,
          DatePosted = p.DatePosted,
          UserName = p.User.Username,
          Tags = p.PostTags.Select(pt => pt.Tag.Name).ToList(),
          Comments = p.Comments.Select(c => new CommentDTO
          {
            CommentId = c.CommentId,
            Content = c.Content,
            DatePosted = c.DatePosted,
            UserId = c.User.UserId,
            UserName = c.User.Username
          }).ToList()
        })
        .ToListAsync();

    return Ok(posts);
  }

  //Search posts by title
  [HttpGet("search/byTitle")]
  public async Task<ActionResult<IEnumerable<PostDTO>>> SearchPostsByTitle([FromQuery] string title)
  {
    var posts = await _context.Posts
        .Include(p => p.User)
        .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
        .Include(p => p.Comments).ThenInclude(c => c.User)
        .Where(p => p.Title.Contains(title))
        .Select(p => new PostDTO
        {
          PostId = p.PostId,
          Title = p.Title,
          Content = p.Content,
          DatePosted = p.DatePosted,
          UserName = p.User.Username,
          Tags = p.PostTags.Select(pt => pt.Tag.Name).ToList(),
          Comments = p.Comments.Select(c => new CommentDTO
          {
            CommentId = c.CommentId,
            Content = c.Content,
            DatePosted = c.DatePosted,
            UserId = c.User.UserId,
            UserName = c.User.Username
          }).ToList()
        })
        .ToListAsync();

    return Ok(posts);
  }

  //Search posts by content
  [HttpGet("search/byContent")]
  public async Task<ActionResult<IEnumerable<PostDTO>>> SearchPostsByContent([FromQuery] string content)
  {
    var posts = await _context.Posts
        .Include(p => p.User)
        .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
        .Include(p => p.Comments).ThenInclude(c => c.User)
        .Where(p => p.Content.Contains(content))
        .Select(p => new PostDTO
        {
          PostId = p.PostId,
          Title = p.Title,
          Content = p.Content,
          DatePosted = p.DatePosted,
          UserName = p.User.Username,
          Tags = p.PostTags.Select(pt => pt.Tag.Name).ToList(),
          Comments = p.Comments.Select(c => new CommentDTO
          {
            CommentId = c.CommentId,
            Content = c.Content,
            DatePosted = c.DatePosted,
            UserId = c.User.UserId,
            UserName = c.User.Username
          }).ToList()
        })
        .ToListAsync();

    return Ok(posts);
  }

  //Create a new post
  [HttpPost]
  public async Task<ActionResult<Post>> CreatePost(CreatePostDTO createPostDto)
  {
    var user = await _context.Users.FindAsync(createPostDto.UserId);
    if (user == null)
    {
      return NotFound("User not found.");
    }

    var newPost = new Post
    {
      Title = createPostDto.Title,
      Content = createPostDto.Content,
      DatePosted = DateTime.UtcNow,
      UserId = createPostDto.UserId,
      UserName = createPostDto.UserName,
      PostTags = new List<PostTag>()
    };

    foreach (var tagName in createPostDto.Tags)
    {
      var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName);
      if (tag == null)
      {
        tag = new Tag { Name = tagName };
        _context.Tags.Add(tag);
      }

      newPost.PostTags.Add(new PostTag { Post = newPost, Tag = tag });
    }

    _context.Posts.Add(newPost);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetPostById), new { id = newPost.PostId }, newPost);
  }

  //Get post by PostId
  [HttpGet("{id}")]
  public async Task<ActionResult<PostDTO>> GetPostById(int id)
  {
    var post = await _context.Posts
        .Include(p => p.User)
        .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
        .Include(p => p.Comments).ThenInclude(c => c.User)
        .Where(p => p.PostId == id)
        .Select(p => new PostDTO
        {
          PostId = p.PostId,
          Title = p.Title,
          Content = p.Content,
          DatePosted = p.DatePosted,
          UserName = p.User.Username,
          Tags = p.PostTags.Select(pt => pt.Tag.Name).ToList(),
          Comments = p.Comments.Select(c => new CommentDTO
          {
            CommentId = c.CommentId,
            Content = c.Content,
            DatePosted = c.DatePosted,
            UserId = c.User.UserId,
            UserName = c.User.Username
          }).ToList()
        })
        .FirstOrDefaultAsync();

    if (post == null)
    {
      return NotFound();
    }

    return Ok(post);
  }

}
