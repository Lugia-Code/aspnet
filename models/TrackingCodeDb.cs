using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;

namespace TrackingCodeApi.models;

public class TrackingCodeDb : DbContext
{
    public TrackingCodeDb(DbContextOptions<TrackingCodeDb> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Setor> Setores => Set<Setor>();
    public DbSet<Localizacao> Localizacoes => Set<Localizacao>();
    public DbSet<Moto> Motos => Set<Moto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("RM558785");
        // Configura relacionamento 1:1 entre Moto e Tag
        modelBuilder.Entity<Moto>()
            .HasOne(m => m.Tag)
            .WithOne(t => t.Moto)
            .HasForeignKey<Moto>(m => m.CodigoTag);

        // Configura relacionamento 1:N entre Tag e Localizacao
        modelBuilder.Entity<Localizacao>()
            .HasOne(l => l.Tag)
            .WithMany(t => t.Localizacoes)
            .HasForeignKey(l => l.CodigoTag);

        // Configura relacionamento 1:N entre Setor e Localizacao
        modelBuilder.Entity<Localizacao>()
            .HasOne(l => l.Setor)
            .WithMany(s => s.Localizacoes)
            .HasForeignKey(l => l.IdSetor);
        // Definindo precisão e escala para decimal
        modelBuilder.Entity<Localizacao>()
            .Property(l => l.X)
            .HasPrecision(18, 6); // 18 dígitos no total, 6 decimais

        modelBuilder.Entity<Localizacao>()
            .Property(l => l.Y)
            .HasPrecision(18, 6);
    }
    
}
