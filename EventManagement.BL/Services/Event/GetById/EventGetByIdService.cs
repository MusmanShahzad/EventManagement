using EventManagement.BL.Services.Event.Get.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.BL.Services.Event.Get
{
    using EventManagement.Models;
    public class EventGetByIdService : IEventGetByIdService
    {
        private readonly MyDbContext dbContext;

        public EventGetByIdService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Event?> GetByIdAsync(Guid Id)
        {
            var internalEventGetService = new InternalEventGetByIdService(dbContext);
            await internalEventGetService.ExtractAsync(Id).ConfigureAwait(true);
            internalEventGetService.TransformAsync();
            return await internalEventGetService.LoadAsync().ConfigureAwait(false);
        }
    }
}
