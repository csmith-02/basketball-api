using System.Net.Mail;
using FullCourtInsights.Auth;
using FullCourtInsights.Data;
using FullCourtInsights.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FullCourtInsights.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(AppDbContext context, AuthService authService) : ControllerBase
    {

        private readonly AppDbContext _context = context;

        private readonly AuthService _authService = authService;

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest user) // Temp User holds all User fields with an extra confirm password field.
        {

            if (user.Name == null || !ValidName(user.Name))
            {
                return BadRequest("Invalid Name.");
            }

            if (user.Email == null || !ValidEmail(user.Email))
            {
                return BadRequest("Invalid Email.");
            }

            if (user.Password == null || user.ConfPassword == null || user.Password != user.ConfPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            User userToAdd = new()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            EntityEntry<User> u = await _context.Users.AddAsync(userToAdd);
            await _context.SaveChangesAsync();

            return Ok($"User with email {u.Entity.Email} has been created.");

        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]        
        public async Task<IActionResult> LoginUser([FromBody] User user)
        {

            if (user.Email == null || !ValidEmail(user.Email))
            {
                return BadRequest("Invalid email");
            }

            if (user.Password == null)
            {
                return BadRequest("Invalid password");
            }

            var retUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

            if (retUser == null)
            {
                return Unauthorized("Email or password was incorrect.");
            }

            var (token, refreshToken, refreshTokenString) = _authService.GenerateToken(retUser.Email!, retUser.Name!);

            RefreshToken rToken = new()
            {
                UserId = retUser.Id,
                Token = refreshTokenString,
                Exp = refreshToken.ValidTo,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };

            await _context.RefreshTokens.AddAsync(rToken);

            await _context.SaveChangesAsync();

            return Ok(new { token, refreshToken = refreshTokenString });
        }

        [HttpGet]
        [Route("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken()
        {

            var token = Request.Headers.Authorization.ToString()["Bearer ".Length..];

            var existingToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);

            var retUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == existingToken!.UserId);

            if (retUser == null)
            {
                return StatusCode(500, "");
            }

            var (retToken, refreshToken, refreshTokenString) = _authService.GenerateToken(retUser.Email!, retUser.Name!);

            await _context.RefreshTokens.AddAsync(new()
            {
                UserId = existingToken!.UserId,
                Token = refreshTokenString,
                Exp = DateTime.UtcNow.AddHours(2),
                CreatedAt = DateTime.UtcNow
            });

            _context.RefreshTokens.RemoveRange(existingToken);
            await _context.SaveChangesAsync();

            return Ok(new { token = retToken, refreshToken = refreshTokenString });

        }

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutUser()
        {

            // Value must be in auth due to Authorize tag
            var auth = Request.Headers.Authorization;

            var token = auth.ToString()["Bearer ".Length..];

            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken == null)
            {
                return Ok("User not logged in.");
            }

            _context.RefreshTokens.RemoveRange(refreshToken);
            await _context.SaveChangesAsync();

            return Ok("User successfully logged out.");

            
        }

        private static bool ValidName(string name)
        {
            if (name.Trim().Replace(" ", "").All(Char.IsLetter))
            {
                return true;
            }
            return false;
        }

        private static bool ValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new(email);

                return true;
            } catch (FormatException)
            {
                return false;
            }
        }
    }
}
