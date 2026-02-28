using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.sistemaVentas;

[Table("producto")]
[Index("IdCategoria", Name = "fk_producto_categoria")]
public partial class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    [Column("nombre")]
    [StringLength(150)]
    public string Nombre { get; set; } = null!;

    [Column("precio", TypeName = "decimal(10,2)")]
    public decimal Precio { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("estado")]
    [StringLength(1)]
    public string Estado { get; set; } = "A";

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime? FechaCreacion { get; set; }

    [ForeignKey("IdCategoria")]
    [InverseProperty("Producto")]
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}