using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.DTOs;
using PruebaJoanAcosta.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Services
{
	public class DeportistaPesoService: IDeportistaPesoService
	{
		private readonly Conexion _context;
		private readonly ILogger _logger;

		public DeportistaPesoService(Conexion conexion, ILogger<DeportistaService> logger)
		{
			_context = conexion;
			_logger = logger;
		}

		public async Task<ActionResult<IEnumerable<DeportistaPeso>>> GetDeportistaPeso()
		{
			return await _context.DeportistaPeso.ToListAsync();
		}

		public async Task<ActionResult<DeportistaPeso>> GetDeportistaPeso(int id)
		{
			var DeportistaPeso = await _context.DeportistaPeso.FindAsync(id);

			return DeportistaPeso;
		}

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

		public async Task<ServiceResponse> PostDeportistaPeso(DeportistaPeso deportistaPeso)
		{
			_logger.LogInformation("creando un nuevo intento del deportista");
			var count = _context.DeportistaPeso.Count(dp => dp.IdDeportistaFk == deportistaPeso.IdDeportistaFk);
			_logger.LogInformation("validando la cantidad de intentos del deportista");
			var response = new ServiceResponse { Code = 200, Message = "Ok." };

			if (count >= 3)
			{
				return response = new ServiceResponse { Code = 500, Message = "Un deportista no puede tener más de tres intentos." };
			}

			_logger.LogInformation("creando el nuevo intento del deportista");
			_context.DeportistaPeso.Add(deportistaPeso);
			var result = await _context.SaveChangesAsync();

			if (result == 0)
			{
				response = new ServiceResponse { Code = 500, Message = "No se pudo crear el deportista" };
			}

			return response;
		}


	}
}
