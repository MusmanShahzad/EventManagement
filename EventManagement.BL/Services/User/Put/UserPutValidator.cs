using EventManagement.BL.CommonDto;
using EventManagement.BL.CommonInterfaces;
using EventManagement.BL.Services.Event.Put.DTO;
using EventManagement.BL.Services.User.Put.DTO;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.BL.Services.Event.Put
{
    public class UserPutValidator : IValidate<UserPutRequestDataDto>
    {
        private readonly MyDbContext dbContext;
        public UserPutValidator(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ValidateDto> ValidateAsync(UserPutRequestDataDto data)
        {
            var validateResponse = new ValidateDto();
            if (data == null)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Missing Input";
                validateResponse.message = "event data is missing";
                return validateResponse;
            }
            if(String.IsNullOrEmpty(data.Email))
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Invalid Input";
                validateResponse.message = "Email is required";
                return validateResponse;
            }
            var uniqueAllergies = data.Allergies.Distinct().ToList();
            if(uniqueAllergies.Count != data.Allergies.Count)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Duplicate";
                validateResponse.message = "Duplicate allergies found";
                return validateResponse;
            }
            var uniqueEventsIds = data.EventIds.Distinct().ToList();
            if(data.EventIds.Count != uniqueEventsIds.Count)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Duplicate";
                validateResponse.message = "Duplicate eventId found";
                return validateResponse;
            }
            var existingUser = await this.dbContext.Users.AnyAsync(x=> x.id == data.Id).ConfigureAwait(false);
            if (!existingUser)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Not found";
                validateResponse.message = "user not found";
                return validateResponse;
            }
            var existingUserEmail = await this.dbContext.Users.AnyAsync(x => x.id != data.Id && x.Email == data.Email).ConfigureAwait(false);
            if (existingUserEmail)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Duplicate";
                validateResponse.message = "user email should be unique";
                return validateResponse;
            }
            var foundUsersCount = await this.dbContext.Events.Where(x => data.EventIds.Contains(x.Id)).CountAsync().ConfigureAwait(false);
            if(foundUsersCount != data.EventIds.Count)
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
