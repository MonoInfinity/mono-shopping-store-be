using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using mono_store_be.Utils.Interface;
using store.UserModule.Entity;
using store.Utils.Interface;

namespace mono_store_be.Utils
{
    public class JwtService : IJwtService
    {
        private readonly string secret;
        private readonly IConfig config;
        public JwtService(IConfig config)
        {
            this.config = config;
            this.secret = this.config.getEnvByKey("JWT_SECRET");
        }

        public string GenerateToken(string data)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("data", data) }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken verifiedToken);

                // I have no idea what am I doing here :)
                var jwtToken = (JwtSecurityToken)verifiedToken;
                return jwtToken.Claims.First(x => x.Type == "data").Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}