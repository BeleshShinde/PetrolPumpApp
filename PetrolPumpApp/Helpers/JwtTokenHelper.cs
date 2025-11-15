using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PetrolPumpApp.Helpers
{
    public static class JwtTokenHelper
    {
        // HARDCODED FOR TESTING - Change these to match exactly
        private const string SecretKey = "MyVeryLongSecretKeyThatIsAtLeast32CharactersLongForHS256Algorithm12345678901234567890";
        private const string Issuer = "PetrolPumpApp";
        private const string Audience = "PetrolPumpAppUsers";
        private const int ExpiryHours = 24;

        public static string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(ExpiryHours),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            System.Diagnostics.Debug.WriteLine($"Generated token for {username}");
            System.Diagnostics.Debug.WriteLine($"Token: {tokenString.Substring(0, 50)}...");

            return tokenString;
        }

        public static ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                System.Diagnostics.Debug.WriteLine($"Token validated successfully for user: {principal.Identity.Name}");

                return principal;
            }
            catch (SecurityTokenExpiredException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Token expired: {ex.Message}");
                return null;
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Invalid signature: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Token validation failed: {ex.GetType().Name} - {ex.Message}");
                return null;
            }
        }
    }
}