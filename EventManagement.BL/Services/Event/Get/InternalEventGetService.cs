namespace EventManagement.BL.Services.Event.Get
{
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.Event.Get.DTO;
    using EventManagement.Models;
    using Microsoft.EntityFrameworkCore;

    public class InternalEventGetService : IGetService<EventDto>
    {
        private MyDbContext dbContext;

        public InternalEventGetService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<EventDto> Data { get; set; }

        public async Task ExtractAsync()
        {
            this.Data = await dbContext.Events.Select(x => new EventDto
            {
                CreatedOn = x.CreatedOn,
                LastUpdatedOn = x.LastUpdatedOn,
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                Users = x.Users.Take(2).ToList(),
                TotalUsers = x.Users.Count()
            }).ToListAsync().ConfigureAwait(false);
        }

        public void TransformAsync()
        {
        }

        public async Task<List<EventDto>> LoadAsync()
        {
            return await Task.FromResult(Data).ConfigureAwait(false);
        }
    }
}
