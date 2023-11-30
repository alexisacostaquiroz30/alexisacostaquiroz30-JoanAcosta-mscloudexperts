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
	public class DeportistaPesoController : ControllerBase
	{

		private readonly Conexion _context;
		private readonly ILogger _logger;

		public DeportistaPesoController(Conexion conexion, ILogger<DeportistaPesoController> logger)
		{
			_context = conexion;
			_logger = logger;
		}


		// GET: api/<DeportistaPesoController>
		/// <summary>
		/// Servicio para obtener todos los arranque, envion de los deportistas
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<DeportistaPeso>>> GetDeportistaPeso()
		{
			return await _context.DeportistaPeso.ToListAsync();
		}

		// GET api/<DeportistaPesoController>/5
		/// <summary>
		/// Servicio para obtener un deportista por medio del id
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<DeportistaPeso>> GetDeportistaPeso(int id)
		{
			var DeportistaPeso = await _context.DeportistaPeso.FindAsync(id);

			if (DeportistaPeso == null)
			{
				return NotFound();
			}

			return DeportistaPeso;
		}

		// GET: api/<DeportistaPesoController>/Intentos
		/// <summary>
		/// Servicio para obtener la cantidad de intentos de cada deportista
		/// </summary>
		/// <returns></returns>
		[HttpGet("Intentos")]
		public async Task<ActionResult<IEnumerable<object>>> Intentos()
		{
			_logger.LogInformation("Consultando cantidad de intentos de los deportistas");
			var deportistaPesoIntentos = await _context.DeportistaPeso
				.Include(dp => dp.Deportista)
				.GroupBy(dp => new { dp.IdDeportistaFk, dp.Deportista.NombreDeportista })
				.Select(group => new
				{
					IdDeportista = group.Key.IdDeportistaFk,
					NombreDeportista = group.Key.NombreDeportista,
					Intentos = group.Count(),
				})
				.ToListAsync();

			return deportistaPesoIntentos;
		}

		/// <summary>
		/// Servicio para obtener los pesos ordenados de mayor a menor
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetMejoresPesos")]
		public async Task<ActionResult<IEnumerable<object>>> GetMejoresPesos()
		{
			_logger.LogInformation("consultando los mejores pesos");
			var mejoresPesos = await _context.DeportistaPeso
				.Include(dp => dp.Deportista)
				.GroupBy(dp => new { dp.IdDeportistaFk, dp.Deportista.NombreDeportista })
				.Select(group => new
				{
					IdDeportista = group.Key.IdDeportistaFk,
					NombreDeportista = group.Key.NombreDeportista,
					MejorArranque = group.Max(dp => dp.Arranque),
					MejorEnvion = group.Max(dp => dp.Envion),
				})
				.Select(dp => new
				{
					dp.IdDeportista,
					dp.NombreDeportista,
					dp.MejorArranque,
					dp.MejorEnvion,
					Total = dp.MejorArranque + dp.MejorEnvion
				})
				.OrderByDescending(dp => dp.Total)
				.ToListAsync();

			return mejoresPesos;
		}

		// POST api/<DeportistaPesoController>
		/// <summary>
		/// Servicio para registrar un intento en las modalidades arranque y envion
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult<DeportistaPeso>> PostDeportistaPeso(DeportistaPeso deportistaPeso)
		{
			_logger.LogInformation("creando un nuevo intento del deportista");
			var count = _context.DeportistaPeso.Count(dp => dp.IdDeportistaFk == deportistaPeso.IdDeportistaFk);
			_logger.LogInformation("validando la cantidad de intentos del deportista");
			if (count >= 3)
			{
				return BadRequest(new { code = 500, msg = "Un deportista no puede tener más de tres intentos." });
			}
			_logger.LogInformation("creando el nuevo intento del deportista");
			_context.DeportistaPeso.Add(deportistaPeso);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetDeportistaPeso), new { id = deportistaPeso.IdDeporPeso }, deportistaPeso);
		}

	}
}
