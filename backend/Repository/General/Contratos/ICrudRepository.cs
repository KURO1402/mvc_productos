namespace SistemaVentas.Repository.General.Contratos
{
    public interface ICrudRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T> Create(T request);
        Task<T> Update(T request);
        Task Delete(int id);
    }
}