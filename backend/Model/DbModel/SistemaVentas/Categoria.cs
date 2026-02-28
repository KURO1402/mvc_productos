using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel.sistemaVentas;

[Table("categoria")]
public partial class Categoria
{
    [Key]
    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Producto> Producto { get; set; } = new List<Producto>();
}