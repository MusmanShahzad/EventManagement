using EventManagement.BL.Services.Event.Get.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.BL.Services.Event.Get
{
    public interface IEventGetService
    {
        Task<List<EventDto>> GetAsync();

    }
}
