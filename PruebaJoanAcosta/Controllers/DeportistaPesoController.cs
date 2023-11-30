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
	public class DeportistaPesoController : ControllerBase
	{

		private readonly IDeportistaPesoService _deportistaPesoService;

		public DeportistaPesoController(IDeportistaPesoService deportistaPesoService)
		{
			_deportistaPesoService = deportistaPesoService;
		}


		// GET: api/<DeportistaPesoController>
		/// <summary>
		/// Servicio para obtener todos los arranque, envion de los deportistas
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<DeportistaPeso>>> GetDeportistaPeso()
		{
			return await _deportistaPesoService.GetDeportistaPeso();
		}

		// GET api/<DeportistaPesoController>/5
		/// <summary>
		/// Servicio para obtener un deportista por medio del id
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<DeportistaPeso>> GetDeportistaPeso(int id)
		{
			var DeportistaPeso = await _deportistaPesoService.GetDeportistaPeso(id);

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
			
			var deportistaPesoIntentos = await _deportistaPesoService.Intentos();

			return deportistaPesoIntentos;
		}

		/// <summary>
		/// Servicio para obtener los pesos ordenados de mayor a menor
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetMejoresPesos")]
		public async Task<ActionResult<IEnumerable<object>>> GetMejoresPesos()
		{
			var mejoresPesos = await _deportistaPesoService.GetMejoresPesos();

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
			var depor = await _deportistaPesoService.PostDeportistaPeso(deportistaPeso);

			if (depor.Code == 500)
			{
				return Unauthorized(new { depor });
			}

			return CreatedAtAction(nameof(GetDeportistaPeso), new { id = deportistaPeso.IdDeporPeso }, deportistaPeso);


		}

	}
}
