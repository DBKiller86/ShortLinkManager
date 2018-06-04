using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLinkManager.Logic.Models
{
    public class ResponseObject
    {
        public bool OperationSucceded { get; set; }
        public string ShortLink { get; set; }
        public string ErrorMessage { get; set; }
        public string Link { get; set; }
    }
}
