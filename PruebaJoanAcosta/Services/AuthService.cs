using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Services
{
	public class AuthService: IAuthService
    {

        private readonly ILogger _logger;
        private readonly Conexion _context;
        private readonly IConfiguration config;

        public AuthService(Conexion conexion, IConfiguration config, ILogger<AuthService> logger)
        {
            _context = conexion;
            this.config = config;
            _logger = logger;
        }

        public async Task<ServiceResponse> Login(LoginDto loginDto)
        {
            _logger.LogInformation("inciando sesion con los siguientes datos");
            _logger.LogInformation(loginDto.CorreoUsuario);
            _logger.LogInformation(loginDto.ContrasenaUsuario);

            var user = await _context.Usuarios.SingleOrDefaultAsync(u => u.CorreoUsuario == loginDto.CorreoUsuario && u.ContrasenaUsuario == loginDto.ContrasenaUsuario);
            var response = new ServiceResponse { Code = 200, Message = "Ok." };

            if (user == null)
            {
                _logger.LogInformation("error al iniciar sesion");
                response = new ServiceResponse { Code = 500, Message = "Credenciales incorrectas." };
			}
			else
			{
                _logger.LogInformation("generando token");
                string jwtToken = GenerarToken(user);
                _logger.LogInformation("finalizo generar token");
                response.Token = jwtToken;
            }

            return response;
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Email, usuario.CorreoUsuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetConnectionString("Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds
                );
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
