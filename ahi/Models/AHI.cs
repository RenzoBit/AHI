namespace ahi.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class AHI : DbContext
	{
		public AHI()
			: base("name=DefaultConnection")
		{
		}

		public virtual DbSet<Dispositivo> Dispositivo { get; set; }
		public virtual DbSet<Ubicacion> Ubicacion { get; set; }
		public virtual DbSet<Usuario> Usuario { get; set; }
		public virtual DbSet<Vehiculo> Vehiculo { get; set; }
		public virtual DbSet<Viaje> Viaje { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Dispositivo>()
				.Property(e => e.mac)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Dispositivo>()
				.HasMany(e => e.viajes)
				.WithRequired(e => e.dispositivo)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Ubicacion>()
				.Property(e => e.latitud)
				.IsUnicode(false);

			modelBuilder.Entity<Ubicacion>()
				.Property(e => e.longitud)
				.IsUnicode(false);

			modelBuilder.Entity<Usuario>()
				.Property(e => e.username)
				.IsUnicode(false);

			modelBuilder.Entity<Usuario>()
				.Property(e => e.password)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Usuario>()
				.Property(e => e.nombre)
				.IsUnicode(false);

			modelBuilder.Entity<Usuario>()
				.HasMany(e => e.viajes)
				.WithMany(e => e.usuarios)
                .Map(m => m.ToTable("ah_usuarioviaje").MapLeftKey("idusuario").MapRightKey("idviaje"));

			modelBuilder.Entity<Vehiculo>()
				.Property(e => e.placa)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Vehiculo>()
				.HasMany(e => e.viajes)
				.WithRequired(e => e.vehiculo)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Viaje>()
				.Property(e => e.descripcion)
				.IsUnicode(false);

			modelBuilder.Entity<Viaje>()
				.HasMany(e => e.ubicacions)
				.WithRequired(e => e.viaje)
				.WillCascadeOnDelete(false);
		}
	}
}