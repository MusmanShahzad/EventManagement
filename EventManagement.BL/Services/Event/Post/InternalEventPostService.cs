using EventManagement.BL.CommonInterfaces;
using EventManagement.BL.Services.Event.Get.DTO;
using EventManagement.BL.Services.Event.Post.DTO;

namespace EventManagement.BL.Services.Event.Post
{
    using EventManagement.Models;
    using Microsoft.EntityFrameworkCore;

    public class InternalEventPostService : IPostService<Event, EventRequestDataDto>
    {
        private readonly MyDbContext dbContext;

        public InternalEventPostService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Event Data { get; set; }

        public EventPostExtractDto extractData { get; set; }

        public async Task ExtractAsync(EventRequestDataDto requestData)
        {
            var users = await this.dbContext.Users.Where(x => requestData.UserIds.Contains(x.id)).ToListAsync().ConfigureAwait(false);
            this.extractData = new EventPostExtractDto
            {
                EventUsers = users,
                RequestData = requestData
            };
        }
        public void TransformAsync()
        {
            this.Data = new Event { 
                Id = Guid.NewGuid(),
                Name = this.extractData.RequestData.Name,
                StartDate = this.extractData.RequestData.StartDate,
                Users = this.extractData.EventUsers,
            };

        }

        public async Task<Event> LoadAsync()
        {
            this.dbContext.Add(Data);
            await this.dbContext.SaveChangesAsync();
            return this.Data;
        }
    }
}
