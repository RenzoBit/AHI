namespace ahi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ah_dispositivo")]
    public partial class Dispositivo
    {
        public Dispositivo()
        {
            viajes = new HashSet<Viaje>();
        }

        [Key]
        public int iddispositivo { get; set; }

        [Required]
        [StringLength(17)]
        public string mac { get; set; }

        public virtual ICollection<Viaje> viajes { get; set; }
    }
}
