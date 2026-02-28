using Microsoft.EntityFrameworkCore;

namespace DbModel.sistemaVentas;

public partial class _sistemaVentasContext : DbContext
{
    public _sistemaVentasContext()
    {
    }

    public _sistemaVentasContext(DbContextOptions<_sistemaVentasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }
    public virtual DbSet<Producto> Producto { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=sistemaVentasDb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.45-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Estado).HasDefaultValue("A");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Producto)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_producto_categoria");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}