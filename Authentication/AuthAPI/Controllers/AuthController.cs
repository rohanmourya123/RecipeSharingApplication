using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly int _jwtExpiryMinutes;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;

            // Read JWT configuration values
            _jwtKey = _config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key");
            _jwtIssuer = _config["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");
            _jwtAudience = _config["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience");
            _jwtExpiryMinutes = int.Parse(_config["Jwt:ExpiryMinutes"] ?? "60");
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model == null)
                return BadRequest("Invalid registration data");

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return Conflict("Email already registered");

            var user = new ApplicationUser
            {
                Email = model.Email,
                Name = model.Name,
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            //return Ok("User registered successfully");
            // Logic for registering the user
            return Ok(new { message = "User registered successfully", user = user });
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("Email and password are required");

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { success = false, message = "Invalid email or password" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { success = false, message = "Invalid email or password" });

            var token = GenerateJwtToken(user);

            //// Store JWT token in session
            //HttpContext.Session.SetString("AuthToken", token);

            return Ok(new { success = true, token });
        }

       
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            //// Clear the token from session
            //HttpContext.Session.Remove("AuthToken");

            return Ok("User logged out successfully");
        }

      
        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("name", user.Name),
                new Claim("username", user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
