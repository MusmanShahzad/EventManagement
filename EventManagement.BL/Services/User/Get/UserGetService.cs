namespace EventManagement.BL.Services.User.Get
{
    using EventManagement.BL.Services.User.Get.DTO;
    public class UserGetService : IUserGetService
    {
        private readonly MyDbContext dbContext;

        public UserGetService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<UserDto>> GetAsync()
        {
            var internalEventGetService = new InternalUserGetService(dbContext);
            await internalEventGetService.ExtractAsync().ConfigureAwait(true);
            internalEventGetService.TransformAsync();
            return await internalEventGetService.LoadAsync().ConfigureAwait(false);
        }
    }
}
