using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace veeb2._0.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Kasutaja> Kasutajad { get; set; }
        public DbSet<Toode> Tooted { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kasutaja>()
                .HasMany(k => k.Tooted)
                .WithOne()
                .HasForeignKey(t => t.KasutajaId);

            modelBuilder.Entity<Kasutaja>().HasData(
                new Kasutaja(1, "kasutaja1", "parool1", "Jaan", "Tamm"),
                new Kasutaja(2, "kasutaja2", "parool2", "Mari", "Kask")
            );

            modelBuilder.Entity<Toode>().HasData(
                new Toode(1, "Toode1", 10.0, true) { KasutajaId = 1 },
                new Toode(2, "Toode2", 15.0, true) { KasutajaId = 1 },
                new Toode(3, "Toode3", 20.0, true) { KasutajaId = 2 },
                new Toode(4, "Toode4", 25.0, false) { KasutajaId = 2 }
            );
        }
    }
}
