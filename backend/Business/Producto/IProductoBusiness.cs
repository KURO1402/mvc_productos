using DtoModel.Producto;

namespace SistemaVentas.Business.Producto
{
    public interface IProductoBusiness
    {
        Task<List<ProductoDto>> GetAll();
        Task<ProductoDto?> GetById(int id);
        Task<ProductoDto> Create(ProductoDto request);
        Task<ProductoDto?> Update(ProductoDto request);
        Task Delete(int id);
    }
}