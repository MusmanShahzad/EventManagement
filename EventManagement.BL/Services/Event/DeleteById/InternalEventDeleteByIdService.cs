namespace EventManagement.BL.Services.Event.DeleteById
{
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.Event.Get.DTO;
    using EventManagement.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class InternalEventDeleteByIdService : IDeleteByIdService<Boolean>
    {
        private MyDbContext dbContext;

        public InternalEventDeleteByIdService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Boolean Data { get; set; }
        public Event extractData { get; set; }

        public async Task ExtractAsync(Guid Id)
        {
            this.extractData = await this.dbContext.Events.SingleOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
        }

        public async Task<Boolean> LoadAsync()
        {
            this.dbContext.Events.Remove(this.extractData);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public void TransformAsync()
        {
        }
    }
}
