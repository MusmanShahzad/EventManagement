namespace EventManagement.BL.Services.User.Put
{
    using EventManagement.BL.CommonDto;
    using EventManagement.BL.Services.User.Put.DTO;
    using EventManagement.Models;
    public interface IUserPutService
    {
        Task<User> PutAsync(UserPutRequestDataDto requestData);
        Task<ValidateDto> Validate(UserPutRequestDataDto requestData);
    }
}
