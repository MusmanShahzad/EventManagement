namespace EventManagement.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using EventManagement.BL.Services.User.Put;
    using EventManagement.BL.Services.User.DeleteById;
    using EventManagement.BL.Services.User.Get;
    using EventManagement.BL.Services.User.Post;
    using EventManagement.BL.Services.User.Put.DTO;
    using EventManagement.BL.Services.User.Post.DTO;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserGetService userService;
        private readonly IUserPostService userPostService;
        private readonly IUserGetByIdService userGetByIdService;
        private readonly IUserPutService userPutService;
        private readonly IUserDeleteByIdService userDeleteByIdService;

        public UserController(
            IUserGetService userService,
            IUserPostService userPostService,
            IUserGetByIdService userGetByIdService,
            IUserPutService userPutService,
            IUserDeleteByIdService userDeleteByIdService)
        {
            this.userService = userService;
            this.userPostService = userPostService;
            this.userGetByIdService = userGetByIdService;
            this.userPutService = userPutService;
            this.userDeleteByIdService = userDeleteByIdService;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? id)
        {
            if (id.HasValue)
            {
                var singleResponseData = await this.userGetByIdService.GetByIdAsync(id.Value).ConfigureAwait(false);
                return new OkObjectResult(singleResponseData);
            }
            var responseData = await this.userService.GetAsync().ConfigureAwait(false);
            return new OkObjectResult(responseData);

        }

        [HttpGet("{id:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var responseData = await this.userService.GetAsync().ConfigureAwait(false);
            return new OkObjectResult(responseData);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequestDataDto requestData)
        {
            var validate = await this.userPostService.Validate(requestData).ConfigureAwait(false);
            if (validate.errorStatus)
            {
                return BadRequest(validate);
            }
            var responseData = await this.userPostService.PostAsync(requestData).ConfigureAwait(true);
            return new OkObjectResult(responseData);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserPutRequestDataDto requestData)
        {
            var validate = await this.userPutService.Validate(requestData).ConfigureAwait(false);
            if (validate.errorStatus)
            {
                return BadRequest(validate);
            }
            var responseData = await this.userPutService.PutAsync(requestData).ConfigureAwait(true);
            return new OkObjectResult(responseData);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById([FromQuery] Guid id)
        {
            var validate = await this.userDeleteByIdService.Validate(id).ConfigureAwait(false);
            if (validate.errorStatus)
            {
                return BadRequest(validate);
            }
            var responseData = await this.userDeleteByIdService.DeleteByIdAsync(id);
            return new OkObjectResult(responseData);
        }

    }
}
