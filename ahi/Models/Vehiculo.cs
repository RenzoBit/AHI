namespace ahi.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

    [Table("ah_vehiculo")]
	public class Vehiculo
	{
		public Vehiculo()
		{
            viajes = new List<Viaje>();
			//viajes = new HashSet<Viaje>();
		}

		[Key]
		public int idvehiculo { get; set; }

		[Required]
		[StringLength(7)]
		public string placa { get; set; }

        public List<Viaje> viajes { get; set; }
		//public virtual ICollection<Viaje> viajes { get; set; }
	}
}
