using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Grupo04.Custom
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration;

        public Utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Método para encriptar una cadena usando SHA256
        public string EncriptarSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                Span<char> hex = new char[bytes.Length * 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    byte b = bytes[i];
                    hex[i * 2] = GetHexValue(b >> 4);
                    hex[i * 2 + 1] = GetHexValue(b & 0x0F);
                }
                return new string(hex);
            }
        }

        // Método auxiliar para convertir un valor a su representación hexadecimal
        public char GetHexValue(int value)
        {
            return (char)(value < 10 ? value + '0' : value - 10 + 'A');
        }

        // Método para generar un JWT usando la información del usuario
        public string GenerarJWT(Usuario usuario)
        {
            var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
        new Claim(ClaimTypes.Role, usuario.Rol ?? "Usuario")
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:ClavePrivada"] ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
