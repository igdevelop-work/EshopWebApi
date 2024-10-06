namespace EshopWebApi.backend.interfaces.base_interfaces;

public interface ITimestamp
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    DateTime? DeletedAt { get; set; }
}