using EventManagement.BL.CommonDto;
using EventManagement.BL.CommonInterfaces;
using EventManagement.BL.Services.Event.Post.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.BL.Services.Event.Post
{
    public class EventPostValidator : IValidate<EventRequestDataDto>
    {
        private readonly MyDbContext dbContext;
        public EventPostValidator(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ValidateDto> ValidateAsync(EventRequestDataDto data)
        {
            var validateResponse = new ValidateDto();
            if (data == null)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Missing Input";
                validateResponse.message = "event data is missing";
                return validateResponse;
            }
            if(data.StartDate < DateTime.Now)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Invalid Input";
                validateResponse.message = "start date should greater than today";
                return validateResponse;
            }
            if(data.UserIds.Count < 2)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Incomplete";
                validateResponse.message = "users should be greater than 2";
                return validateResponse;
            }
            var uniqueUserIds = data.UserIds.Distinct().ToList();
            if(data.UserIds.Count != uniqueUserIds.Count)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Duplicate";
                validateResponse.message = "Duplicate userId found";
                return validateResponse;
            }
            var foundUsersCount = await this.dbContext.Users.Where(x => data.UserIds.Contains(x.id)).CountAsync().ConfigureAwait(false);
            if(foundUsersCount != data.UserIds.Count)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Not found";
                validateResponse.message = "users can't found";
                return validateResponse;
            }  
            else
            {
                var result = await this.dbContext.Events.Where(x=> x.Name == data.Name).AnyAsync().ConfigureAwait(false);
                if(result)
                {
                    validateResponse.errorStatus = true;
                    validateResponse.error = "Duplicate";
                    validateResponse.message = "Event name should be unique";
                }

            }
            return validateResponse;
        }
    }
}
