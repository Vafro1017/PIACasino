using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PIACasino.Entidades;

namespace PIACasino
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Relacion>().HasKey(R => new { R.RifaId, R.ParticipanteId });
        }

        public DbSet<Rifa> Rifas { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Relacion> Relaciones { get; set;}
        public DbSet<Premio> Premios { get; set;}
    }
}
