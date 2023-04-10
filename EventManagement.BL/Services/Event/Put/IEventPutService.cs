namespace EventManagement.BL.Services.Event.Put
{
    using EventManagement.BL.CommonDto;
    using EventManagement.BL.Services.Event.Post.DTO;
    using EventManagement.BL.Services.Event.Put.DTO;
    using EventManagement.Models;
    public interface IEventPutService
    {
        Task<Event> PutAsync(EventPutRequestDataDto requestData);
        Task<ValidateDto> Validate(EventPutRequestDataDto requestData);
    }
}
