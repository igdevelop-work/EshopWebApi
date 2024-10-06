namespace EshopWebApi.backend.interfaces.base_interfaces.entity_layer;

public interface IWriteService<T>
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(Guid id);
}