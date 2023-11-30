using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaJoanAcosta.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsuariosController : ControllerBase
	{

		private readonly Conexion _context;


		public UsuariosController(Conexion conexion)
		{
			_context = conexion;
		}


		// GET: api/<UsuariosController>
		/// <summary>
		/// Servicio para obtener todos los usuarios
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
		{
			return await _context.Usuarios.ToListAsync();
		}

		// GET api/<UsuariosController>/5
		/// <summary>
		/// Servicio para obtener un usuario
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Usuario>> GetUsuario(int id)
		{
			var Usuario = await _context.Usuarios.FindAsync(id);

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
			_context.Usuarios.Add(Usuario);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetUsuario), new { id = Usuario.IdUsuario }, Usuario);
		}

	}
}
