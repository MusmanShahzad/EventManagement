namespace EventManagement.BL.Services.Event.Get
{
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.Event.Get.DTO;
    using EventManagement.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class InternalEventGetByIdService : IGetByIdService<Event>
    {
        private MyDbContext dbContext;

        public InternalEventGetByIdService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Event? Data { get; set; }

        public async Task ExtractAsync(Guid Id)
        {
            this.Data = await dbContext.Events.Where(x => x.Id == Id).Select(x => new Event
            {
                CreatedOn = x.CreatedOn,
                LastUpdatedOn = x.LastUpdatedOn,
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                Users = x.Users
            }).SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public void TransformAsync()
        {
        }

        public async Task<Event> LoadAsync()
        {
            return await Task.FromResult(Data).ConfigureAwait(false);
        }
    }
}
