namespace EventManagement.BL.Services.User.Put
{
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.User.Put.DTO;
    using EventManagement.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class InternalUserPutService : IPostService<User, UserPutRequestDataDto>
    {
        private readonly MyDbContext dbContext;

        public InternalUserPutService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User Data { get; set; }

        public UserPutExtractDto extractData { get; set; }

        public async Task ExtractAsync(UserPutRequestDataDto requestData)
        {
            // Extract existing event and selected events
            var existingUser = await this.dbContext.Users
                .Include(e => e.Events)
                .Include(e => e.Allergies)
                .FirstOrDefaultAsync(e => e.id == requestData.Id)
                .ConfigureAwait(false);

            var selectedEvent = await this.dbContext.Events
                .Where(u => requestData.EventIds.Contains(u.Id))
                .ToListAsync()
                .ConfigureAwait(false);

            this.extractData = new UserPutExtractDto
            {
                ExistingUser = existingUser,
                SelectedEvents = selectedEvent,
                RequestData = requestData
            };
        }

        public void TransformAsync()
        {
            // Modify existing event with new data and users
            this.extractData.ExistingUser.Name = this.extractData.RequestData.Name;
            this.extractData.ExistingUser.Email = this.extractData.RequestData.Email;
            this.extractData.ExistingUser.Events.Clear();
            this.extractData.ExistingUser.Events.AddRange(this.extractData.SelectedEvents);
            // Remove any existing allergies that are not in the new list
            var allergiesToRemove = this.extractData.ExistingUser.Allergies.Where(a => !this.extractData.RequestData.Allergies.Any(a2 => a2 == a.Name)).ToList();
            allergiesToRemove.ForEach(a => this.extractData.RequestData.Allergies.Remove(a.Name));
            allergiesToRemove.ForEach(a => {
                this.dbContext.Allergies.Remove(a);
                this.extractData.ExistingUser.Allergies.Remove(a);
                });

            // Add any new allergies that are not already in the user's list
            var allergiesToAdd = this.extractData.RequestData.Allergies.Where(a => !this.extractData.ExistingUser.Allergies.Any(a2 => a2.Name == a)).ToList();
            allergiesToAdd.ForEach(a => {
                var newAllergy = new Allergy
                {
                    Id = Guid.NewGuid(),
                    Name = a
                };
                this.dbContext.Allergies.Add(newAllergy);
                this.extractData.ExistingUser.Allergies.Add(newAllergy);
                });

            this.Data = this.extractData.ExistingUser;
        }

        public async Task<User> LoadAsync()
        {
            // Save changes to database
            await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
            return this.Data;
        }
    }
}