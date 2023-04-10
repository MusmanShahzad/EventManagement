namespace EventManagement.BL.Services.Event.Post
{
    using EventManagement.BL.CommonDto;
    using EventManagement.BL.Services.Event.Post.DTO;
    using EventManagement.Models;
    public interface IEventPostService
    {
        Task<Event> PostAsync(EventRequestDataDto requestData);

        Task<ValidateDto> Validate(EventRequestDataDto requestData);
    }
}
