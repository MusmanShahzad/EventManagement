
namespace EventManagement.BL.Services.Event.Get
{
    using EventManagement.Models;
    public interface IEventGetByIdService
    {
        Task<Event?> GetByIdAsync(Guid Id);

    }
}
