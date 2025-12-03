using Common.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepository<User> repository;
        private readonly IConfiguration config;

        public LoginController(IRepository<User> repository, IConfiguration config)
        {
            this.repository = repository;
            this.config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser userLogin)
        {
            var user = await Authenticate(userLogin);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(new
                {
                    token = token,
                    user = new
                    {
                        id = user.Id,
                        name = user.FirstName + " " + user.LastName,
                        email = user.Email
                    }
                });
            }
            return BadRequest("user does not exist");
        }


        private string Generate(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.FirstName + user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> Authenticate(LoginUser loginUser)
        {
            var users = await repository.GetAll();
            string hashedInputPassword = HashPassword(loginUser.password);

            return users.FirstOrDefault(x => x.Email == loginUser.email && x.Password == hashedInputPassword);
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

    }
}
