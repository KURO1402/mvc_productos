using DtoModel.Producto;
using SistemaVentas.Repository.ProductoRepo.Contratos;

namespace SistemaVentas.Business.Producto
{
    public class ProductoBusiness : IProductoBusiness
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoBusiness(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoDto> Create(ProductoDto request)
        {
            ProductoDto result = await _productoRepository.Create(request);
            return result;
        }

        public async Task<List<ProductoDto>> GetAll()
        {
            List<ProductoDto> lista = await _productoRepository.GetAll();
            return lista;
        }

        public async Task<ProductoDto?> GetById(int id)
        {
            ProductoDto? producto = await _productoRepository.GetById(id);
            return producto;
        }

        public async Task<ProductoDto?> Update(ProductoDto request)
        {
            ProductoDto? productoDB = await _productoRepository.GetById(request.IdProducto);
            if (productoDB == null)
            {
                throw new Exception("Producto a actualizar no existe");
            }
            ProductoDto result = await _productoRepository.Update(request);
            return result;
        }

        public async Task Delete(int id)
        {
            await _productoRepository.Delete(id);
        }
    }
}