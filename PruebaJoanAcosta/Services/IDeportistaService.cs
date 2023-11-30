using Microsoft.AspNetCore.Mvc;
using PruebaJoanAcosta.DTOs;
using PruebaJoanAcosta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Services
{
	public interface IDeportistaService
	{
		Task<ActionResult<IEnumerable<Deportista>>> GetDeportistas();
		Task<ActionResult<Deportista>> GetDeportista(int id);
		Task<ServiceResponse> PostDeportista(Deportista deportista);
	}
}
