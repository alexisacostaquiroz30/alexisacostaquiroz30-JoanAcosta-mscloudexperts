using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.Models;
using PruebaJoanAcosta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaJoanAcosta.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DeportistasController : ControllerBase
	{
		private readonly IDeportistaService _deportistaService;

		public DeportistasController(IDeportistaService deportistaService)
		{
			_deportistaService = deportistaService;
		}


		// GET: api/<DeportistasController>
		/// <summary>
		/// Servicio para obtener la informacion de todos los deportistas
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Deportista>>> GetDeportistas()
		{
			return await _deportistaService.GetDeportistas();
		}

		// GET api/<DeportistasController>/5
		/// <summary>
		/// Servicio para obtener la ifnromacion de un deportista
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Deportista>> GetDeportista(int id)
		{
			var deportista =  await _deportistaService.GetDeportista(id);

			if (deportista == null)
			{
				return NotFound();
			}

			return deportista;
		}

		// POST api/<DeportistasController>
		/// <summary>
		/// Servicio para registar un deportista
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<Deportista>> PostDeportista(Deportista deportista)
		{
			var depor = await _deportistaService.PostDeportista(deportista);

			if (depor.Code == 500)
			{
				return Unauthorized(new { depor });
			}


			return CreatedAtAction(nameof(GetDeportista), new { id = deportista.IdDeportista }, deportista);
		}

	}
}
