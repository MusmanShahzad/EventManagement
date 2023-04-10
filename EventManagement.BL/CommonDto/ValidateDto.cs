using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.BL.CommonDto
{
    public class ValidateDto
    {
        public Boolean errorStatus = false;
        public string message { get; set; }
        public string error { get; set; }
    }
}
