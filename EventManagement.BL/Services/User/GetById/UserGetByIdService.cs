namespace EventManagement.BL.Services.User.Get
{
    using EventManagement.BL.Services.User.Get.DTO;
    public class UserGetByIdService : IUserGetByIdService
    {
        private readonly MyDbContext dbContext;

        public UserGetByIdService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserDto?> GetByIdAsync(Guid Id)
        {
            var internalEventGetService = new InternalUserGetByIdService(dbContext);
            await internalEventGetService.ExtractAsync(Id).ConfigureAwait(true);
            internalEventGetService.TransformAsync();
            return await internalEventGetService.LoadAsync().ConfigureAwait(false);
        }
    }
}
