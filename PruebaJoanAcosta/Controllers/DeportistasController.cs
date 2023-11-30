using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.Models;
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

		private readonly Conexion _context;
		private readonly ILogger _logger;

		public DeportistasController(Conexion conexion, ILogger<DeportistasController> logger)
		{
			_context = conexion;
			_logger = logger;
		}


		// GET: api/<DeportistasController>
		/// <summary>
		/// Servicio para obtener la informacion de todos los deportistas
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Deportista>>> GetDeportistas()
		{
			_logger.LogInformation("consultando los deportistas");
			return await _context.Deportistas.ToListAsync();
		}

		// GET api/<DeportistasController>/5
		/// <summary>
		/// Servicio para obtener la ifnromacion de un deportista
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Deportista>> GetDeportista(int id)
		{
			var deportista = await _context.Deportistas.FindAsync(id);

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
			_logger.LogInformation("creando un nuevo deportista");
			_context.Deportistas.Add(deportista);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetDeportista), new { id = deportista.IdDeportista }, deportista);
		}

	}
}
