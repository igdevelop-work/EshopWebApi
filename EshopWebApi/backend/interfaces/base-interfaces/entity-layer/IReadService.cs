using EshopWebApi.backend.shared.entities;

namespace EshopWebApi.backend.interfaces.base_interfaces.entity_layer;

public interface IReadService<T>
{
    IQueryable<ProductEntity> FindAll();
    Task<T> FindById(Guid id);
    Task<IEnumerable<T>> FindAllPaged(int page, int pageSize);
}