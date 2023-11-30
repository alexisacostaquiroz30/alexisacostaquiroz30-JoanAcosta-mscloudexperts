using Microsoft.AspNetCore.Mvc;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Services
{
	public interface IUsuarioService
	{
		Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios();
		Task<ActionResult<Usuario>> GetUsuario(int id);
		Task<ServiceResponse> PostUsuario(Usuario Usuario);
	}
}