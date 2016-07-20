namespace ahi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ah_ubicacion")]
    public partial class Ubicacion
    {
        [Key]
        public int idubicacion { get; set; }

        public int idviaje { get; set; }

        [Required]
        [StringLength(20)]
        public string latitud { get; set; }

        [Required]
        [StringLength(20)]
        public string longitud { get; set; }

        public DateTime hora { get; set; }

        public virtual Viaje viaje { get; set; }
    }
}
