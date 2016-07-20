namespace ahi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ah_usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            viajes = new HashSet<Viaje>();
        }

        [Key]
        public int idusuario { get; set; }

        [Required]
        [StringLength(12)]
        public string username { get; set; }

        [Required]
        [StringLength(32)]
        public string password { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

		public virtual ICollection<Viaje> viajes { get; set; }

		public const short ADMINISTRADOR = 1;
    }
}
