using EventManagement.BL.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.BL.CommonInterfaces
{
    public interface IValidate<TInputData>
            where TInputData : new()
    {
        Task<ValidateDto> ValidateAsync(TInputData data);
    }
}
