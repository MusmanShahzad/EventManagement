
namespace EventManagement.BL.Services.Event.DeleteById
{
    using EventManagement.BL.CommonDto;
    using EventManagement.Models;
    public interface IEventDeleteByIdService
    {
        Task<ValidateDto> Validate(Guid Id);
        Task<Boolean> DeleteByIdAsync(Guid Id);

    }
}
