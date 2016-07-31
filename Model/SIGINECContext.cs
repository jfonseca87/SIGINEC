namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SIGINECContext : DbContext
    {
        public SIGINECContext()
            : base("name=SIGINECContext")
        {
        }

        public DbSet<Bitacora> Bitacora { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Detalle_Bitacora> Detalle_Bitacora { get; set; }
        public DbSet<Detalle_Solicitud_BajoStock> Detalle_Solicitud_BajoStock { get; set; }
        public DbSet<Dispositivo> Dispositivo { get; set; }
        public DbSet<Estado_Dispositivo> Estado_Dispositivo { get; set; }
        public DbSet<Estados_Op> Estados_Op { get; set; }
        public DbSet<Ingreso_Dispositivo> Ingreso_Dispositivo { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Seguimiento_BajoStock> Seguimiento_BajoStock { get; set; }
        public DbSet<Seguimiento_SolDispositivo> Seguimiento_SolDispositivo { get; set; }
        public DbSet<Solicitud_BajoStock> Solicitud_BajoStock { get; set; }
        public DbSet<Solicitud_Dispositivo> Solicitud_Dispositivo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Menu1> Menu1 { get; set; }
        public DbSet<Menu2> Menu2 { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bitacora>()
                .HasMany(e => e.Detalle_Bitacora)
                .WithOptional(e => e.Bitacora)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Solicitud_Dispositivo)
                .WithOptional(e => e.Cliente)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Dispositivo>()
                .HasMany(e => e.Bitacora)
                .WithRequired(e => e.Dispositivo)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Dispositivo>()
                .HasMany(e => e.Detalle_Solicitud_BajoStock)
                .WithOptional(e => e.Dispositivo)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Dispositivo>()
                .HasMany(e => e.Ingreso_Dispositivo)
                .WithOptional(e => e.Dispositivo)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Dispositivo>()
                .HasMany(e => e.Solicitud_Dispositivo)
                .WithOptional(e => e.Dispositivo)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Estado_Dispositivo>()
                .HasMany(e => e.Bitacora)
                .WithRequired(e => e.Estado_Dispositivo)
                .HasForeignKey(e => e.Id_Estado_Dispositivo)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Estados_Op>()
                .HasMany(e => e.Solicitud_Dispositivo)
                .WithOptional(e => e.Estados_Op)
                .HasForeignKey(e => e.Estado_Solicitud)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Estados_Op>()
                .HasMany(e => e.Solicitud_BajoStock)
                .WithOptional(e => e.Estados_Op)
                .HasForeignKey(e => e.Estado_Solicitud)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Cliente)
                .WithOptional(e => e.Persona)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Persona>()
                .HasMany(e => e.Usuario)
                .WithOptional(e => e.Persona)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Solicitud_BajoStock>()
                .HasMany(e => e.Detalle_Solicitud_BajoStock)
                .WithOptional(e => e.Solicitud_BajoStock)
                .HasForeignKey(e => e.Id_Solicitud_Stock)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Solicitud_BajoStock>()
                .HasMany(e => e.Seguimiento_BajoStock)
                .WithOptional(e => e.Solicitud_BajoStock)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Solicitud_Dispositivo>()
                .HasMany(e => e.Seguimiento_SolDispositivo)
                .WithOptional(e => e.Solicitud_Dispositivo)
                .HasForeignKey(e => e.Id_SolicitudDisp)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Bitacora)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_Registra)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Ingreso_Dispositivo)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_Registra)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Solicitud_BajoStock)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_SolBajoStock);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Solicitud_Dispositivo)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_SolDispositivo);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Seguimiento_SolDispositivo)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_Seguimiento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Seguimiento_BajoStock)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_Seguimiento)
                .WillCascadeOnDelete(false);
        }
    }
}
