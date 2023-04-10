using EventManagement.BL.Services.Event.Post.DTO;

namespace EventManagement.BL.Services.Event.Post
{
    using EventManagement.BL.CommonDto;
    using EventManagement.BL.Services.Event.DeleteById;
    using EventManagement.Models;
    public class EventPostService: IEventPostService
    {
        private readonly MyDbContext dbContext;

        public EventPostService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<ValidateDto> Validate(EventRequestDataDto requestData)
        {
            var validate = new EventPostValidator(dbContext);
            return await validate.ValidateAsync(requestData).ConfigureAwait(false);
        }

        public async Task<Event> PostAsync(EventRequestDataDto requestData)
        {
            var internalPostEventService = new InternalEventPostService(dbContext);
            await internalPostEventService.ExtractAsync(requestData).ConfigureAwait(false);
            internalPostEventService.TransformAsync();
            return await internalPostEventService.LoadAsync().ConfigureAwait(false);
        }
    }
}
