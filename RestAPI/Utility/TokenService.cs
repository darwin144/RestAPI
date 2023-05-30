using Microsoft.IdentityModel.Tokens;
using RestAPI.Others;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestAPI.Utility
{
    public interface ITokenService {
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromxpiredToken(string token);
        ClaimVM ExtractClaimsFromJwt(string token);
    }
    public class TokenService : ITokenService
    {
        //untuk mendapatkan data dari json
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClaimVM ExtractClaimsFromJwt(string token)
        {
            if (token.IsNullOrEmpty()) return new ClaimVM();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,  // untuk cek masa berlaku token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
                };
                // parse and validate
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (securityToken != null && claimsPrincipal.Identity is ClaimsIdentity identity)
                {

                    var claims = new ClaimVM
                    {
                        NameIdentifier = identity.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                        Name = identity.FindFirst(ClaimTypes.Name)!.Value,
                        Email = identity.FindFirst(ClaimTypes.Email)!.Value
                    };

                    var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(claims => claims.Value).ToList();

                    claims.Roles = roles;
                    return claims;
                }
            }
            catch
            {
                return new ClaimVM();
            }

            return new ClaimVM();
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            // generate by secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWT:Issuer"],
                                                    audience: _configuration["JWT:Audience"],
                                                    claims: claims,
                                                    expires: DateTime.Now.AddMinutes(10),
                                                    signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            
            return tokenString;
        }

        public ClaimsPrincipal GetPrincipalFromxpiredToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
