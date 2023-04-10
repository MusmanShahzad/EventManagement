namespace EventManagement.BL.Services.User.Post
{
    using EventManagement.BL.CommonDto;
    using EventManagement.Models;
    using EventManagement.BL.Services.User.Post.DTO;

    public class UserPostService: IUserPostService
    {
        private readonly MyDbContext dbContext;

        public UserPostService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<ValidateDto> Validate(UserRequestDataDto requestData)
        {
            var validate = new UserPostValidator(dbContext);
            return await validate.ValidateAsync(requestData).ConfigureAwait(false);
        }

        public async Task<User> PostAsync(UserRequestDataDto requestData)
        {
            var internalPostEventService = new InternalUserPostService(dbContext);
            await internalPostEventService.ExtractAsync(requestData).ConfigureAwait(false);
            internalPostEventService.TransformAsync();
            return await internalPostEventService.LoadAsync().ConfigureAwait(false);
        }
    }
}
