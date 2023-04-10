namespace EventManagement.BL.Services.User.Get
{
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.User.Get.DTO;
    using Microsoft.EntityFrameworkCore;

    public class InternalUserGetService : IGetService<UserDto>
    {
        private MyDbContext dbContext;

        public InternalUserGetService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<UserDto> Data { get; set; }

        public async Task ExtractAsync()
        {
            this.Data = await dbContext.Users.Select(x => new UserDto
            {
                Id = x.id,
                Name = x.Name,
                Email = x.Email,
                Event = x.Events.Take(2).ToList(),
                TotalEvent = x.Events.Count(),
                Allergies = x.Allergies.Take(2).ToList(),
                TotalAllergies = x.Allergies.Count(),

            }).ToListAsync().ConfigureAwait(false);
        }

        public void TransformAsync()
        {
        }

        public async Task<List<UserDto>> LoadAsync()
        {
            return await Task.FromResult(Data).ConfigureAwait(false);
        }
    }
}
