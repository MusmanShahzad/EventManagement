namespace EventManagement.BL.Services.User.Put
{
    using EventManagement.BL.CommonDto;
    using EventManagement.BL.Services.Event.Put;
    using EventManagement.BL.Services.User.Put.DTO;
    using EventManagement.Models;
    public class UserPutService : IUserPutService
    {
        private readonly MyDbContext dbContext;

        public UserPutService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> PutAsync(UserPutRequestDataDto requestData)
        {
            var internalPostEventService = new InternalUserPutService(dbContext);
            await internalPostEventService.ExtractAsync(requestData).ConfigureAwait(false);
            internalPostEventService.TransformAsync();
            return await internalPostEventService.LoadAsync().ConfigureAwait(false);
        }

        public async Task<ValidateDto> Validate(UserPutRequestDataDto requestData)
        {
            var validate = new UserPutValidator(dbContext);
            return await validate.ValidateAsync(requestData).ConfigureAwait(false);
        }
    }
}
