using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;

namespace TrackingCodeApi.models
{
    public class TrackingCodeDb : DbContext
    {
        public TrackingCodeDb(DbContextOptions<TrackingCodeDb> options) : base(options)
        {
        }


        public DbSet<Auditoria> Auditoria => Set<Auditoria>();
        public DbSet<Tag> Tag => Set<Tag>();
        public DbSet<Setor> Setor => Set<Setor>();
        public DbSet<Localizacao> Localizacao => Set<Localizacao>();
        public DbSet<Moto> Moto => Set<Moto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RM558785");

           
            modelBuilder.Entity<Tag>()
                .HasOne(t => t.Moto)
                .WithOne() 
                .HasForeignKey<Tag>(t => t.Chassi) 
                .OnDelete(DeleteBehavior.SetNull); 

           
            modelBuilder.Entity<Localizacao>()
                .HasOne(l => l.Tag) 
                .WithMany(t => t.Localizacoes) 
                .HasForeignKey(l => l.CodigoTag) 
                .OnDelete(DeleteBehavior.Cascade); 

            
            modelBuilder.Entity<Localizacao>()
                .HasOne(l => l.Setor) 
                .WithMany(s => s.Localizacoes) 
                .HasForeignKey(l => l.IdSetor) 
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Localizacao>()
                .Property(l => l.X)
                .HasPrecision(18, 6); 
            modelBuilder.Entity<Localizacao>()
                .Property(l => l.Y)
                .HasPrecision(18, 6);
        }
    }
}
