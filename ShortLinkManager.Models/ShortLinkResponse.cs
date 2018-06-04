using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLinkManager.Models
{
    public class ShortLinkResponse
    {
        public bool OperationSucceded { get; set; }
        public string ShortLink { get; set; }
        public string ErrorMessage { get; set; }
        public string Link { get; set; }
    }
}
