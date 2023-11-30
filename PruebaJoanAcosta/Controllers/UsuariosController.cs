using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaJoanAcosta.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsuariosController : ControllerBase
	{

		private readonly IUsuarioService _usuarioService;


		public UsuariosController(IUsuarioService usuarioService)
		{
			_usuarioService = usuarioService;
		}


		// GET: api/<UsuariosController>
		/// <summary>
		/// Servicio para obtener todos los usuarios
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
		{
			return await _usuarioService.GetUsuarios();
		}

		// GET api/<UsuariosController>/5
		/// <summary>
		/// Servicio para obtener un usuario
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Usuario>> GetUsuario(int id)
		{
			var Usuario = await _usuarioService.GetUsuario(id);

			if (Usuario == null)
			{
				return NotFound();
			}

			return Usuario;
		}

		// POST api/<UsuariosController>
		/// <summary>
		/// Servicio para crear un usuario
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<Usuario>> PostUsuario(Usuario Usuario)
		{
			
			var user = await _usuarioService.PostUsuario(Usuario);

			if (user.Code == 500)
			{
				return Unauthorized(new {user});
			}

			return CreatedAtAction(nameof(GetUsuario), new { id = Usuario.IdUsuario }, Usuario);
		}

	}
}
