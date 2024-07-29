using JMF_Web_Forum_API.Data;
using JMF_Web_Forum_API.DTO;
using JMF_Web_Forum_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Moderator")] // Ensure only users with Moderator role can access this controller
public class PostTagsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PostTagsController(AppDbContext context)
    {
        _context = context;
    }

    // Add tags to a post
    [HttpPost("add")]
    public async Task<ActionResult> AddTagsToPost([FromBody] AddTagsDTO addTagsDto)
    {
        var post = await _context.Posts.FindAsync(addTagsDto.PostId);
        if (post == null)
        {
            return NotFound("Post not found.");
        }

        foreach (var tagName in addTagsDto.Tags)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName);
            if (tag == null)
            {
                tag = new Tag { Name = tagName };
                _context.Tags.Add(tag);
            }

            if (!_context.PostTags.Any(pt => pt.PostId == addTagsDto.PostId && pt.TagId == tag.TagId))
            {
                var postTag = new PostTag
                {
                    PostId = addTagsDto.PostId,
                    TagId = tag.TagId
                };
                _context.PostTags.Add(postTag);
            }
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    // Remove tags from a post
    [HttpPost("remove")]
    public async Task<ActionResult> RemoveTagsFromPost([FromBody] RemoveTagsDTO removeTagsDto)
    {
        var post = await _context.Posts.FindAsync(removeTagsDto.PostId);
        if (post == null)
        {
            return NotFound("Post not found.");
        }

        foreach (var tagName in removeTagsDto.Tags)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName);
            if (tag != null)
            {
                var postTag = await _context.PostTags
                    .FirstOrDefaultAsync(pt => pt.PostId == removeTagsDto.PostId && pt.TagId == tag.TagId);

                if (postTag != null)
                {
                    _context.PostTags.Remove(postTag);
                }
            }
        }

        await _context.SaveChangesAsync();
        return Ok();
    }
}


