using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZalohovacServer.Models;

namespace ZalohovacServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientLoginController : ControllerBase
    {
        
        private readonly IConfiguration configuration;
        public ClientJob clientJob = new ClientJob();

        public ClientLoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("login")]

        public string Post(string name)
        {
            bool exists = true;
            try
            {
                Client client = clientJob.GetClients().First(x => x.name == name);
            }
            catch
            {
                exists = false;
            }
            
            

            if(exists)
            {
                return GenerateToken(name);
            }else
            {
                this.clientJob.AddClient(new Client() { name = name, active = true });
                return GenerateToken(name);
            }
            
        }


        private string GenerateToken(string username)
        {
            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,username),
                    new Claim(ClaimTypes.Role, "Client")
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("JwtSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

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

