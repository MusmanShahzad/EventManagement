using EventManagement.BL.CommonDto;
using EventManagement.BL.CommonInterfaces;
using EventManagement.BL.Services.Event.Put.DTO;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.BL.Services.Event.Put
{
    public class EventPutValidator : IValidate<EventPutRequestDataDto>
    {
        private readonly MyDbContext dbContext;
        public EventPutValidator(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ValidateDto> ValidateAsync(EventPutRequestDataDto data)
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
            var existingEvent = await this.dbContext.Events.AnyAsync(x=> x.Id == data.Id).ConfigureAwait(false);
            if (!existingEvent)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Not found";
                validateResponse.message = "event not found";
                return validateResponse;
            }
            var existingEventName = await this.dbContext.Events.AnyAsync(x => x.Id != data.Id && x.Name == data.Name).ConfigureAwait(false);
            if (existingEventName)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Duplicate";
                validateResponse.message = "event name should be unique";
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
            return validateResponse;
        }
    }
}
