using EventManagement.BL.CommonDto;
using EventManagement.BL.CommonInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.BL.Services.Event.DeleteById
{
    public class DeleteByIdValidator : IValidate<Guid>
    {
        private readonly MyDbContext dbContext;
        public DeleteByIdValidator(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ValidateDto> ValidateAsync(Guid data)
        {
            var validateResponse = new ValidateDto();
            if (data == null)
            {
                validateResponse.error = "Missing Input";
                validateResponse.message = "Id is required";
            }
            else
            {
                var result = await this.dbContext.Events.Where(x=> x.Id == data).AnyAsync().ConfigureAwait(false);
                if(!result)
                {
                    validateResponse.errorStatus = true;
                    validateResponse.error = "Not Found";
                    validateResponse.message = "event not found";
                }

            }
            return validateResponse;
        }
    }
}
