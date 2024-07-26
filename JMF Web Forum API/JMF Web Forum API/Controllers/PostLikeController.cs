using JMF_Web_Forum_API.Data;
using JMF_Web_Forum_API.DTO;
using JMF_Web_Forum_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class PostLikesController : ControllerBase
{
    private readonly AppDbContext _context;

    public PostLikesController(AppDbContext context)
    {
        _context = context;
    }

    // Like or dislike a post
    [HttpPost]
    public async Task<ActionResult> LikeOrDislikePost(PostLikeDTO createPostLikeDto)
    {
        var user = await _context.Users.FindAsync(createPostLikeDto.UserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var post = await _context.Posts
            .Include(p => p.User) // Include the author of the post
            .FirstOrDefaultAsync(p => p.PostId == createPostLikeDto.PostId);

        if (post == null)
        {
            return NotFound("Post not found.");
        }

        // Ensure user is not trying to like/dislike their own post
        if (post.UserId == createPostLikeDto.UserId)
        {
            return BadRequest("You cannot like or dislike your own post.");
        }

        // Check if the user has already liked or disliked the post
        var existingPostLike = await _context.PostLikes
            .Where(pl => pl.UserId == createPostLikeDto.UserId && pl.PostId == createPostLikeDto.PostId)
            .SingleOrDefaultAsync();

        if (existingPostLike != null)
        {
            // Update the existing like/dislike
            existingPostLike.IsLike = createPostLikeDto.IsLike;
        }
        else
        {
            // Add a new like/dislike
            var newPostLike = new PostLike
            {
                PostId = createPostLikeDto.PostId,
                UserId = createPostLikeDto.UserId,
                IsLike = createPostLikeDto.IsLike
            };

            _context.PostLikes.Add(newPostLike);
        }

        await _context.SaveChangesAsync();
        return Ok();
    }
}
