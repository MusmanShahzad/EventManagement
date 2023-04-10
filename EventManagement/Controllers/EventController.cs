using EventManagement.BL.Services.Event.DeleteById;
using EventManagement.BL.Services.Event.Get;
using EventManagement.BL.Services.Event.Post;
using EventManagement.BL.Services.Event.Post.DTO;
using EventManagement.BL.Services.Event.Put;
using EventManagement.BL.Services.Event.Put.DTO;
using EventManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EventManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventGetService eventService;
        private readonly IEventPostService eventPostService;
        private readonly IEventGetByIdService eventGetByIdService;
        private readonly IEventPutService eventPutService;
        private readonly IEventDeleteByIdService eventDeleteByIdService;

        public EventController(IEventGetService eventService, IEventPostService eventPostService, IEventGetByIdService eventGetByIdService, IEventPutService eventPutService, IEventDeleteByIdService eventDeleteByIdService)
        {
            this.eventService = eventService;
            this.eventPostService = eventPostService;
            this.eventGetByIdService = eventGetByIdService;
            this.eventPutService = eventPutService;
            this.eventDeleteByIdService = eventDeleteByIdService;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? id)
        {
            if (id.HasValue)
            {
                var singleResponseData = await this.eventGetByIdService.GetByIdAsync(id.Value).ConfigureAwait(false);
                return new OkObjectResult(singleResponseData);
            }
            var responseData = await this.eventService.GetAsync().ConfigureAwait(false);
            return new OkObjectResult(responseData);

        }

        [HttpGet("{id:guid}", Name = "GetEventById")]
        public async Task<IActionResult> GetEventById([FromRoute] Guid id)
        {
            var responseData = await this.eventService.GetAsync().ConfigureAwait(false);
            return new OkObjectResult(responseData);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EventRequestDataDto requestData)
        {
            var validate = await this.eventPostService.Validate(requestData).ConfigureAwait(false);
            if (validate.errorStatus)
            {
                return BadRequest(validate);
            }
            var responseData = await this.eventPostService.PostAsync(requestData).ConfigureAwait(true);
            return new OkObjectResult(responseData);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EventPutRequestDataDto requestData)
        {
            var validate = await this.eventPutService.Validate(requestData).ConfigureAwait(false);
            if (validate.errorStatus)
            {
                return BadRequest(validate);
            }
            var responseData = await this.eventPutService.PutAsync(requestData).ConfigureAwait(true);
            return new OkObjectResult(responseData);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById([FromQuery] Guid id)
        {
            var validate = await this.eventDeleteByIdService.Validate(id).ConfigureAwait(false);
            if (validate.errorStatus)
            {
                return BadRequest(validate);
            }
            var responseData = await this.eventDeleteByIdService.DeleteByIdAsync(id);
            return new OkObjectResult(responseData);
        }
    }
}