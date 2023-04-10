namespace EventManagement.BL.Services.Event.Put
{
    using EventManagement.BL.CommonDto;
    using EventManagement.BL.Services.Event.Put.DTO;
    using EventManagement.Models;
    public class EventPutService : IEventPutService
    {
        private readonly MyDbContext dbContext;

        public EventPutService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Event> PutAsync(EventPutRequestDataDto requestData)
        {
            var internalPostEventService = new InternalEventPutService(dbContext);
            await internalPostEventService.ExtractAsync(requestData).ConfigureAwait(false);
            internalPostEventService.TransformAsync();
            return await internalPostEventService.LoadAsync().ConfigureAwait(false);
        }

        public async Task<ValidateDto> Validate(EventPutRequestDataDto requestData)
        {
            var validate = new EventPutValidator(dbContext);
            return await validate.ValidateAsync(requestData).ConfigureAwait(false);
        }
    }
}
