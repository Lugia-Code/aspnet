using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;

namespace TrackingCodeApi.models
{
    public class TrackingCodeDb : DbContext
    {
        public TrackingCodeDb(DbContextOptions<TrackingCodeDb> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Auditoria> Auditoria => Set<Auditoria>();
        public DbSet<Tag> Tag => Set<Tag>();
        public DbSet<Setor> Setor => Set<Setor>();
        public DbSet<Localizacao> Localizacao => Set<Localizacao>();
        public DbSet<Moto> Moto => Set<Moto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RM558785");

            // Relacionamento 1:1 entre Moto e Tag (A TAG tem a chave estrangeira 'Chassi')
            modelBuilder.Entity<Tag>()
                .HasOne(t => t.Moto) // Uma TAG tem uma MOTO
                .WithOne() // Uma MOTO tem uma TAG (associação inversa não é necessária)
                .HasForeignKey<Tag>(t => t.Chassi) // A chave estrangeira está na TAG (campo 'Chassi')
                .OnDelete(DeleteBehavior.SetNull); // Se a MOTO for deletada, a TAG terá 'Chassi' igual a NULL

            // Relacionamento 1:N entre Tag e Localizacao (Uma TAG pode ter várias LOCALIZACOES)
            modelBuilder.Entity<Localizacao>()
                .HasOne(l => l.Tag) // Localização tem uma TAG
                .WithMany(t => t.Localizacoes) // TAG tem várias LOCALIZACOES
                .HasForeignKey(l => l.CodigoTag) // A chave estrangeira está em Localizacao
                .OnDelete(DeleteBehavior.Cascade); // Se a TAG for deletada, todas as Localizações associadas serão deletadas

            // Relacionamento 1:N entre Setor e Localizacao (Um SETOR pode ter várias LOCALIZACOES)
            modelBuilder.Entity<Localizacao>()
                .HasOne(l => l.Setor) // Localização tem um Setor
                .WithMany(s => s.Localizacoes) // Setor tem várias Localizações
                .HasForeignKey(l => l.IdSetor) // A chave estrangeira está em Localizacao
                .OnDelete(DeleteBehavior.Cascade); // Se o Setor for deletado, todas as Localizações associadas serão deletadas

            // Definindo precisão e escala para as propriedades de localização (X e Y)
            modelBuilder.Entity<Localizacao>()
                .Property(l => l.X)
                .HasPrecision(18, 6); // 18 dígitos no total, 6 decimais

            modelBuilder.Entity<Localizacao>()
                .Property(l => l.Y)
                .HasPrecision(18, 6);
        }
    }
}
