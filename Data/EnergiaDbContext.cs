using Microsoft.EntityFrameworkCore;
using EnergiaApi.Models;

namespace EnergiaApi.Data
{
    public class EnergiaDbContext : DbContext
    {
        public EnergiaDbContext(DbContextOptions<EnergiaDbContext> options)
            : base(options)
        {
        }

        public DbSet<ConsumoEnergia> Consumos { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações do modelo ConsumoEnergia
            modelBuilder.Entity<ConsumoEnergia>()
                .Property(c => c.Local)
                .IsRequired();

            modelBuilder.Entity<ConsumoEnergia>()
                .Property(c => c.Equipamento)
                .IsRequired();

            // Configurações do modelo Alerta
            modelBuilder.Entity<Alerta>()
                .Property(a => a.Mensagem)
                .IsRequired();

            modelBuilder.Entity<Alerta>()
                .Property(a => a.Local)
                .IsRequired();

            // Configurações do modelo Sensor
            modelBuilder.Entity<Sensor>()
                .Property(s => s.Nome)
                .IsRequired();

            modelBuilder.Entity<Sensor>()
                .Property(s => s.Local)
                .IsRequired();

            modelBuilder.Entity<Sensor>()
                .Property(s => s.Tipo)
                .IsRequired();

            // Configurações do modelo Usuario
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nome)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Senha)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
} 