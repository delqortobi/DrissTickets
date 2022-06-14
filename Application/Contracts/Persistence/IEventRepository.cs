using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IEventRepository:IAsyncRepository<Event>
{
    Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate);
}