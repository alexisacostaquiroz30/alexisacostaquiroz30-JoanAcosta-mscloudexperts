using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Models
{
	public class Deportista
	{
		[Key]
		public int IdDeportista { get; set; }
		public string PaisDeportista { get; set; }
		public string NombreDeportista { get; set; }

		public virtual ICollection<DeportistaPeso> DeportistaDeportistaPesos { get; set; }
	}
}
