using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;

namespace TrackingCodeApi.models;

public class TrackingCodeDb(DbContextOptions<TrackingCodeDb> options) : DbContext(options)
{
 
  public DbSet<Usuario> Usuarios => Set<Usuario>();
  public DbSet<Auditoria> Auditorias => Set<Auditoria>();
  public DbSet<Tag> Tags => Set<Tag>();
  
  public DbSet<Setor> Setores => Set<Setor>();
  public DbSet<Localizacao> Localizacoes => Set<Localizacao>();
  
  public DbSet<Moto> Motos => Set<Moto>();
}