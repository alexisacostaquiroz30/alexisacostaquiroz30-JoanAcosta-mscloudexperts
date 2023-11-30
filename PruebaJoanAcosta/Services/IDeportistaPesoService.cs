using Microsoft.AspNetCore.Mvc;
using PruebaJoanAcosta.DTOs;
using PruebaJoanAcosta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Services
{
	public interface IDeportistaPesoService
	{
		Task<ActionResult<IEnumerable<DeportistaPeso>>> GetDeportistaPeso();
		Task<ActionResult<DeportistaPeso>> GetDeportistaPeso(int id);
		Task<ActionResult<IEnumerable<object>>> Intentos();
		Task<ActionResult<IEnumerable<object>>> GetMejoresPesos();
		Task<ServiceResponse> PostDeportistaPeso(DeportistaPeso deportistaPeso);
	}
}
