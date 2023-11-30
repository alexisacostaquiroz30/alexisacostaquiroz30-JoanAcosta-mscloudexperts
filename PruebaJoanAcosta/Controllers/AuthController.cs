using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PruebaJoanAcosta.Data;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Conexion _context;
        private readonly IConfiguration config;
        private readonly ILogger _logger;

        public AuthController(Conexion conexion, IConfiguration config, ILogger<AuthController> logger)
        {
            _context = conexion;
            this.config = config;
            _logger = logger;
        }

        /// <summary>
        /// Servicio de Login | devulve el token o error de credenciales
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            _logger.LogInformation("inciando sesion con los siguientes datos");
            _logger.LogInformation(loginDto.CorreoUsuario);
            _logger.LogInformation(loginDto.ContrasenaUsuario);

            var user = await _context.Usuarios.SingleOrDefaultAsync(u => u.CorreoUsuario == loginDto.CorreoUsuario && u.ContrasenaUsuario == loginDto.ContrasenaUsuario);

            if (user == null)
            {
                _logger.LogInformation("error al iniciar sesion");
                return Unauthorized(new { code = 500, msg = "Credenciales incorrectas." });
            }

            _logger.LogInformation("generando token");
            string jwtToken = GenerarToken(user);
            _logger.LogInformation("finalizo generar token");

            return Ok(new { token = jwtToken });
        }

        private string GenerarToken (Usuario usuario)
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
