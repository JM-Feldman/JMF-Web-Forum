using JMF_Web_Forum_API.Data;
using JMF_Web_Forum_API.DTO;
using JMF_Web_Forum_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CommentsController(AppDbContext context)
    {
        _context = context;
    }

    //Add a comment to a post
    [HttpPost]
    public async Task<ActionResult<Comment>> AddComment(CreateCommentDTO createCommentDto)
    {
        var user = await _context.Users.FindAsync(createCommentDto.UserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var post = await _context.Posts.FindAsync(createCommentDto.PostId);
        if (post == null)
        {
            return NotFound("Post not found.");
        }

        var newComment = new Comment
        {
            Content = createCommentDto.Content,
            DatePosted = DateTime.UtcNow,
            UserId = createCommentDto.UserId,
            PostId = createCommentDto.PostId
        };

        _context.Comments.Add(newComment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCommentById), new { id = newComment.CommentId }, newComment);
    }

  //Get comment by ID
  [HttpGet("{id}")]
  public async Task<ActionResult<CommentDTO>> GetCommentById(int id)
  {
    var comment = await _context.Comments
        .Include(c => c.User)
        .Include(c => c.Post)
        .Where(c => c.CommentId == id)
        .Select(c => new CommentDTO
        {
          CommentId = c.CommentId,
          Content = c.Content,
          DatePosted = c.DatePosted,
          UserId = c.User.UserId,
          UserName = c.User.Username,
          PostId = c.Post.PostId,
          PostTitle = c.Post.Title
        })
        .FirstOrDefaultAsync();

    if (comment == null)
    {
      return NotFound();
    }

    return Ok(comment);
  }
}
