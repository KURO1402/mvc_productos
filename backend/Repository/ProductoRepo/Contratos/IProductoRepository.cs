using DtoModel.Producto;
using SistemaVentas.Repository.General.Contratos;

namespace SistemaVentas.Repository.ProductoRepo.Contratos
{
    public interface IProductoRepository : ICrudRepository<ProductoDto>, IDisposable
    {
    }
}