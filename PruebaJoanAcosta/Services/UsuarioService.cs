using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Services
{
	public class UsuarioService: IUsuarioService
	{

		private readonly Conexion _context;
		public UsuarioService(Conexion context)
		{
			_context = context;
		}

		public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
		{
			return await _context.Usuarios.ToListAsync();
		}

		public async Task<ActionResult<Usuario>> GetUsuario(int id)
		{
			var Usuario = await _context.Usuarios.FindAsync(id);

			return Usuario;
		}

		public async Task<ServiceResponse> PostUsuario(Usuario Usuario)
		{
			_context.Usuarios.Add(Usuario);
			var result = await _context.SaveChangesAsync();
			var response = new ServiceResponse { Code = 200, Message = "Ok." };

			if (result==0)
			{
				response = new ServiceResponse { Code = 500, Message = "Credenciales Incorrectas" };
			}

			return response;


		}



	}
}
