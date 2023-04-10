namespace EventManagement.BL.Services.User.Get
{
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.User.Get.DTO;
    using Microsoft.EntityFrameworkCore;

    public class InternalUserGetByIdService : IGetByIdService<UserDto>
    {
        private MyDbContext dbContext;

        public InternalUserGetByIdService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserDto? Data { get; set; }

        public async Task ExtractAsync(Guid Id)
        {
            this.Data = await dbContext.Users.Where(x => x.id == Id).Select(x => new UserDto
            {
                Id = x.id,
                Name = x.Name,
                Email = x.Email,
                Event = x.Events,
                TotalEvent = x.Events.Count,
                Allergies = x.Allergies,
                TotalAllergies = x.Allergies.Count,
            }).SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public void TransformAsync()
        {
        }

        public async Task<UserDto> LoadAsync()
        {
            return await Task.FromResult(Data).ConfigureAwait(false);
        }
    }
}
