using bts_test.Models;
using bts_test.Database;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;

namespace bts_test.Services
{
    public class AuthServices
    {
        private readonly SqlLiteQuery _db;
        private readonly IConfiguration _config;

        public AuthServices(SqlLiteQuery db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public LoginResponseModel Login(LoginRequestModel model)
        {
            var user = _db.GetUserByUsername(model.Username);
            if (user == null)
            {
                throw new NullReferenceException("User Not Found");
            }

            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (!isCorrectPassword)
            {
                throw new UnauthorizedAccessException("Wrong Password");
            }

            return new LoginResponseModel
            {
                JwtToken = CreateJwtToken(user)
            };
        }

        public string CreateJwtToken(UserModel user)
        {
            var algorithm = SecurityAlgorithms.HmacSha256;
            var claimsPayload = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var signature = _config.GetSection("AppSettings:Key").Value;
            var encodeSignature = Encoding.UTF8.GetBytes(signature);

            var token = new JwtSecurityToken(
                claims: claimsPayload,
                issuer: _config.GetSection("AppSettings:Issuer").Value,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(encodeSignature), algorithm)
            );

            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return serializedToken;
        }

        public bool Register(RegistrationRequestModel model)
        {
            try {
                _db.RegisterUser(new UserModel {
                    Email = model.Email,
                    Username = model.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
                });
                return true;
            } catch {
                return false;
            } 

        }
    }
}
