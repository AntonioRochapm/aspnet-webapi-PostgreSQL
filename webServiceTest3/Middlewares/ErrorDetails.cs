using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webServiceTest3.Middlewares
{
    public class ErrorDetails
    {
        public short StatusCode { get; set; }
        public DateTime At { get; set; } = DateTime.Now;
        public string Message { get; set; }

    }
}
