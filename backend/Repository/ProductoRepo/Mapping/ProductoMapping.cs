using DbModel.sistemaVentas;
using DtoModel.Producto;

namespace SistemaVentas.Repository.ProductoRepo.Mapping
{
    public static class ProductoMapping
    {
        public static ProductoDto ToDto(this Producto producto)
        {
            return new ProductoDto
            {
                IdProducto = producto.IdProducto,
                IdCategoria = producto.IdCategoria,
                NombreCategoria = producto.IdCategoriaNavigation?.Nombre,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Estado = producto.Estado,
                FechaCreacion = producto.FechaCreacion
            };
        }

        public static Producto ToEntity(this ProductoDto productoDto)
        {
            return new Producto
            {
                IdProducto = productoDto.IdProducto,
                IdCategoria = productoDto.IdCategoria,
                Nombre = productoDto.Nombre,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock,
                Estado = productoDto.Estado,
                FechaCreacion = productoDto.FechaCreacion
            };
        }

        public static List<ProductoDto> ToDtoList(this List<Producto> productos)
        {
            return productos.Select(p => p.ToDto()).ToList();
        }

        public static List<Producto> ToEntityList(this List<ProductoDto> productoDtos)
        {
            return productoDtos.Select(p => p.ToEntity()).ToList();
        }
    }
}