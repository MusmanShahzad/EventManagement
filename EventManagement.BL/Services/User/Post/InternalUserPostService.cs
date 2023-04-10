namespace EventManagement.BL.Services.User.Post
{
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.User.Post.DTO;
    using EventManagement.Models;
    using Microsoft.EntityFrameworkCore;

    public class InternalUserPostService : IPostService<User, UserRequestDataDto>
    {
        private readonly MyDbContext dbContext;

        public InternalUserPostService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public User Data { get; set; }

        public UserPostExtractDto extractData { get; set; }

        public async Task ExtractAsync(UserRequestDataDto requestData)
        {
            var userEvents = await this.dbContext.Events.Where(x => requestData.EventIds.Contains(x.Id)).ToListAsync().ConfigureAwait(false);
            var userAllergies = requestData.Allergies.Select(x => new Allergy
            {
                Id = Guid.NewGuid(),
                Name = x
            }).ToList();
            this.extractData = new UserPostExtractDto
            {
                UserAllergies = userAllergies,
                UsersEvents = userEvents,
                RequestData = requestData
            };
        }
        public void TransformAsync()
        {
            this.Data = new User { 
                id = Guid.NewGuid(),
                Name = this.extractData.RequestData.Name,
                Email = this.extractData.RequestData.Email,
                Events = this.extractData.UsersEvents,
                Allergies = this.extractData.UserAllergies
            };

        }

        public async Task<User> LoadAsync()
        {
            this.dbContext.Add(Data);
            await this.dbContext.SaveChangesAsync();
            return this.Data;
        }
    }
}
