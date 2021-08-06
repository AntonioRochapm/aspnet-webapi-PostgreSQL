using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webServiceTest3.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
        public string LogEvent { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }
    }
}
