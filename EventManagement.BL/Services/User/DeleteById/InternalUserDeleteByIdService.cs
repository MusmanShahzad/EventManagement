namespace EventManagement.BL.Services.User.DeleteById
{
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.Event.Get.DTO;
    using EventManagement.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class InternalUserDeleteByIdService : IDeleteByIdService<Boolean>
    {
        private MyDbContext dbContext;

        public InternalUserDeleteByIdService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Boolean Data { get; set; }
        public User extractData { get; set; }

        public async Task ExtractAsync(Guid Id)
        {
            this.extractData = await this.dbContext.Users.SingleOrDefaultAsync(x => x.id == Id).ConfigureAwait(false);
        }

        public async Task<Boolean> LoadAsync()
        {
            this.dbContext.Users.Remove(this.extractData);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public void TransformAsync()
        {
        }
    }
}
