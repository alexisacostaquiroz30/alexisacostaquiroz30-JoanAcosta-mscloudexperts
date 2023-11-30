using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Models
{
	public class DeportistaPeso
	{
		[Key]
		public int IdDeporPeso { get; set; }
		[ForeignKey("Deportista")]
		public int IdDeportistaFk { get; set; }
		
		public int Arranque { get; set; }	
		public int Envion { get; set; }

		public virtual Deportista Deportista { get; set; }
	}
}
