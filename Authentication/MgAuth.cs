using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MachManager.Authentication
{
    public class MgAuth
    {
        private readonly string _key;
        public MgAuth(string key)
        {
            this._key = key;
        }
        public string Authenticate(bool authenticated, string userName, MgAuthType authType)
        {
            // create token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // set token key
            var tokenKey = System.Text.Encoding.ASCII.GetBytes(this._key);

            // create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.Role, authType.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // obtain token string
            var tokenStr = tokenHandler.WriteToken(token);

            return tokenStr;
        }
    }
}