using DbModel.sistemaVentas;
using DtoModel.Producto;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Repository.ProductoRepo.Contratos;
using SistemaVentas.Repository.ProductoRepo.Mapping;

namespace SistemaVentas.Repository.ProductoRepo.Implementacion
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly _sistemaVentasContext _db;

        public ProductoRepository(_sistemaVentasContext db)
        {
            _db = db;
        }

        public async Task<ProductoDto> Create(ProductoDto request)
        {
            Producto producto = ProductoMapping.ToEntity(request);
            await _db.Producto.AddAsync(producto);
            await _db.SaveChangesAsync();
            request = ProductoMapping.ToDto(producto);
            return request;
        }

        public async Task Delete(int id)
        {
            await _db.Producto.Where(x => x.IdProducto == id).ExecuteDeleteAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<ProductoDto>> GetAll()
        {
            List<Producto> data = await _db.Producto
                .Include(x => x.IdCategoriaNavigation)
                .ToListAsync();
            return ProductoMapping.ToDtoList(data);
        }

        public async Task<ProductoDto?> GetById(int id)
        {
            Producto? producto = await _db.Producto
                .Include(x => x.IdCategoriaNavigation)
                .Where(x => x.IdProducto == id)
                .FirstOrDefaultAsync();
            if (producto == null) return null;
            return ProductoMapping.ToDto(producto);
        }

        public async Task<ProductoDto> Update(ProductoDto request)
        {
            var producto = await _db.Producto.FindAsync(request.IdProducto);
            try
            {
                if (producto == null)
                {
                    throw new Exception("Producto no encontrado");
                }

                producto.IdCategoria = request.IdCategoria;
                producto.Nombre = request.Nombre;
                producto.Precio = request.Precio;
                producto.Stock = request.Stock;
                producto.Estado = request.Estado;

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ProductoMapping.ToDto(producto);
        }
    }
}