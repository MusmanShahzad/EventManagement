using EventManagement.BL.Services.Event.Get.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.BL.Services.Event.Get
{
    public class EventGetService : IEventGetService
    {
        private readonly MyDbContext dbContext;

        public EventGetService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<EventDto>> GetAsync()
        {
            var internalEventGetService = new InternalEventGetService(dbContext);
            await internalEventGetService.ExtractAsync().ConfigureAwait(true);
            internalEventGetService.TransformAsync();
            return await internalEventGetService.LoadAsync().ConfigureAwait(false);
        }
    }
}
