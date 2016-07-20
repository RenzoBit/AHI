namespace ahi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ah_viaje")]
    public partial class Viaje
    {
        public Viaje()
        {
            ubicacions = new HashSet<Ubicacion>();
            usuarios = new HashSet<Usuario>();
        }

        [Key]
        public int idviaje { get; set; }

        public int iddispositivo { get; set; }

        public int idvehiculo { get; set; }

        [Required]
        [StringLength(100)]
        public string descripcion { get; set; }

        public DateTime horainicio { get; set; }

        public DateTime? horafin { get; set; }

        [NotMapped]
        public string mac { set; get; }

        public virtual Dispositivo dispositivo { get; set; }

        public virtual ICollection<Ubicacion> ubicacions { get; set; }

        public virtual Vehiculo vehiculo { get; set; }

        public virtual ICollection<Usuario> usuarios { get; set; }
    }
}
