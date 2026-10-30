using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ZalohovacServer.Models; 

namespace ZalohovacServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminControllerReal : ControllerBase
    {
        public static Admin admin = new Admin();
        public MyContext _context = new MyContext();
        private readonly IConfiguration configuration;

        public AdminControllerReal(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<Admin> Register (AdminDTO request)
        {
            string passwrodHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            admin.login = request.Username;

            admin.passwordHash = passwrodHash;

            _context.Admins.Add(admin);
            _context.SaveChanges(); 
            return Ok(admin); 
        }

        [HttpPost("login")]
        public ActionResult<Admin> Login(AdminDTO request)
        {
            if (request == null)
            {
                return BadRequest("Request is null");
            }

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username or Password is missing");
            }

            try
            {
                admin = _context.Admins.First(x => x.login == request.Username);
            }
            catch
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, admin.passwordHash))
            {
                return BadRequest("Wrong Password");
            }

            string token = GenerateToken(admin);

            return Ok(token);
        }
        private string GenerateToken(Admin admin)
        {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.login),
                    new Claim(ClaimTypes.Role, "Admin") 
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("JwtSettings:Token").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt; 
        }
    }
}
