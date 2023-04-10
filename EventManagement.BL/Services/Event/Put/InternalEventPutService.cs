namespace EventManagement.BL.Services.Event.Put
{
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.Event.Put.DTO;
    using EventManagement.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class InternalEventPutService : IPostService<Event, EventPutRequestDataDto>
    {
        private readonly MyDbContext dbContext;

        public InternalEventPutService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Event Data { get; set; }

        public EventPutExtractDto extractData { get; set; }

        public async Task ExtractAsync(EventPutRequestDataDto requestData)
        {
            // Extract existing event and selected users
            var existingEvent = await this.dbContext.Events
                .Include(e => e.Users)
                .FirstOrDefaultAsync(e => e.Id == requestData.Id)
                .ConfigureAwait(false);

            if (existingEvent == null)
            {
                throw new ArgumentException($"Event with Id {requestData.Id} not found.");
            }

            var selectedUsers = await this.dbContext.Users
                .Where(u => requestData.UserIds.Contains(u.id))
                .ToListAsync()
                .ConfigureAwait(false);

            this.extractData = new EventPutExtractDto
            {
                ExistingEvent = existingEvent,
                SelectedUsers = selectedUsers,
                RequestData = requestData
            };
        }

        public void TransformAsync()
        {
            // Modify existing event with new data and users
            this.extractData.ExistingEvent.Name = this.extractData.RequestData.Name;
            this.extractData.ExistingEvent.StartDate = this.extractData.RequestData.StartDate;
            this.extractData.ExistingEvent.LastUpdatedOn = DateTime.Now;
            this.extractData.ExistingEvent.Users.Clear();
            this.extractData.ExistingEvent.Users.AddRange(this.extractData.SelectedUsers);

            this.Data = this.extractData.ExistingEvent;
        }

        public async Task<Event> LoadAsync()
        {
            // Save changes to database
            await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
            return this.Data;
        }
    }
}