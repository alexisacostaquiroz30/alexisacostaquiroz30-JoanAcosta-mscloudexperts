using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Data
{
	public class Usuario
	{
		[Key]
		public int IdUsuario { get; set; }
		public string NombreUsuario { get; set; }
		public string CorreoUsuario { get; set; }
		public string ContrasenaUsuario { get; set; }
	}
}
