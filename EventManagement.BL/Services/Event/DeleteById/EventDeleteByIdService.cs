using EventManagement.BL.Services.Event.Get.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.BL.Services.Event.DeleteById
{
    using EventManagement.BL.CommonDto;
    using EventManagement.Models;
    public class EventDeleteByIdService : IEventDeleteByIdService
    {
        private readonly MyDbContext dbContext;

        public EventDeleteByIdService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> DeleteByIdAsync(Guid Id)
        {
            var internalEventGetService = new InternalEventDeleteByIdService(dbContext);
            await internalEventGetService.ExtractAsync(Id).ConfigureAwait(true);
            internalEventGetService.TransformAsync();
            return await internalEventGetService.LoadAsync().ConfigureAwait(false);
        }

        public async Task<ValidateDto> Validate(Guid Id)
        {
            var validate = new DeleteByIdValidator(dbContext);
            return await validate.ValidateAsync(Id).ConfigureAwait(false);
        }
    }
}
