using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaJoanAcosta.Data;
using PruebaJoanAcosta.DTOs;
using PruebaJoanAcosta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Services
{
	public class DeportistaService: IDeportistaService
	{
		private readonly Conexion _context;
		private readonly ILogger _logger;
		public DeportistaService(Conexion conexion, ILogger<DeportistaService> logger)
		{
			_context = conexion;
			_logger = logger;
		}

		public async Task<ActionResult<IEnumerable<Deportista>>> GetDeportistas()
		{
			_logger.LogInformation("consultando los deportistas");
			return await _context.Deportistas.ToListAsync();
		}

		public async Task<ActionResult<Deportista>> GetDeportista(int id)
		{
			var deportista = await _context.Deportistas.FindAsync(id);

			return deportista;
		}

		public async Task<ServiceResponse> PostDeportista(Deportista deportista)
		{
			_logger.LogInformation("creando un nuevo deportista");
			_context.Deportistas.Add(deportista);
			
			var result = await _context.SaveChangesAsync();
			var response = new ServiceResponse { Code = 200, Message = "Ok." };

			if (result == 0)
			{
				response = new ServiceResponse { Code = 500, Message = "No se pudo crear el deportista" };
			}

			return response;

		}

	}
}
