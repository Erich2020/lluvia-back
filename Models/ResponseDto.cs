using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempporalWS.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; } = true;

        public Object Result { get; set; }

        public string DisplayMessage { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}
