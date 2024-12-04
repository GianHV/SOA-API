using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AuthorizationServer.Models;
using Common.Base;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly string _connectionString;
        private readonly string _key;
        private readonly string _issuer;

        public AuthController(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:DbConnectionString").Get<string>() ?? string.Empty;
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
        }
        [HttpPost]
        public IActionResult Login([FromBody] UserDTO request)
        {
            User user = GetUser(request);
            if (user == null) return APIResponse(user, HttpStatusCode.Unauthorized);
            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            // JWT Security Token Parameters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return APIResponse(new JwtSecurityTokenHandler().WriteToken(token));
        }

        private User GetUser(UserDTO request)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                var parameters = new DynamicParameters();
                parameters.Add("@email", request.Email);
                parameters.Add("@password", request.Password);
                var result = conn.Query<User>("sp_login", parameters, null,commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
    }
}
