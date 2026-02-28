namespace DtoModel.Producto
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Estado { get; set; } = "A";
        public DateTime? FechaCreacion { get; set; }
    }
}