using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using JMF_Web_Forum_API.Models;
using JMF_Web_Forum_API.DTO;
using JMF_Web_Forum_API.Data;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        public static User user = new User();
        private readonly IConfiguration _configuration;
        public string token = string.Empty;

        public AuthController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register"), AllowAnonymous]
        public ActionResult<LoginResponse> Register(RegisterUserDTO request)
        {
            var existingUser = _context.Users.SingleOrDefault(x => x.Username == request.Username);
            if (existingUser != null)
            {
                return BadRequest("Invalid Username or Password");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User newUser = new User
            {
                Username = request.Username,
                Password = passwordHash,
                Userrole = "Customer"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();


            var token = CreateToken(newUser);


            var response = new LoginResponse
            {
                Username = newUser.Username,
                Token = token,
                Role = newUser.Userrole
            };


            return Ok(response);
        }

        [HttpPost("login"), AllowAnonymous]
        public ActionResult<LoginResponse> Login(LoginUserDTO request)
        {
            if (request == null)
            {
                return NoContent();
            }

            var currentUser = _context.Users.SingleOrDefault(x => x.Username == request.Username);

            if (currentUser == null || !BCrypt.Net.BCrypt.Verify(request.Password, currentUser.Password))
            {
                return BadRequest("Username or password not found.");
            }

            var token = CreateToken(currentUser);

            var response = new LoginResponse
            {
                Username = currentUser.Username,
                Token = token,
                Role = currentUser.Userrole
            };

            return Ok(response);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Userrole)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
