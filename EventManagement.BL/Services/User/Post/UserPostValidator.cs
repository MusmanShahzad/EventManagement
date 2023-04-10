namespace EventManagement.BL.Services.User.Post
{
    using EventManagement.BL.CommonDto;
    using EventManagement.BL.CommonInterfaces;
    using EventManagement.BL.Services.User.Post.DTO;
    using Microsoft.EntityFrameworkCore;
    public class UserPostValidator : IValidate<UserRequestDataDto>
    {
        private readonly MyDbContext dbContext;
        public UserPostValidator(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ValidateDto> ValidateAsync(UserRequestDataDto data)
        {
            var validateResponse = new ValidateDto();
            if (data == null)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Missing Input";
                validateResponse.message = "user data is missing";
                return validateResponse;
            }
            if(String.IsNullOrEmpty(data.Email))
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Invalid Input";
                validateResponse.message = "Invalid email";
                return validateResponse;
            }
            var uniqueAllergies = data.Allergies.Distinct().ToList();
            if (uniqueAllergies.Count != data.Allergies.Count)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Duplicate";
                validateResponse.message = "Duplicate allergies found";
                return validateResponse;
            }
            var uniqueEventIds = data.EventIds.Distinct().ToList();
            if(data.EventIds.Count != uniqueEventIds.Count)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Duplicate";
                validateResponse.message = "Duplicate event found";
                return validateResponse;
            }
            var foundEventCount = await this.dbContext.Events.Where(x => data.EventIds.Contains(x.Id)).CountAsync().ConfigureAwait(false);
            if(foundEventCount != data.EventIds.Count)
            {
                validateResponse.errorStatus = true;
                validateResponse.error = "Not found";
                validateResponse.message = "event can't found";
                return validateResponse;
            }
            else
            {
                var result = await this.dbContext.Users.Where(x=> x.Email == data.Email).AnyAsync().ConfigureAwait(false);
                if(result)
                {
                    validateResponse.errorStatus = true;
                    validateResponse.error = "Duplicate";
                    validateResponse.message = "Email should be unique";
                }

            }
            return validateResponse;
        }
    }
}
