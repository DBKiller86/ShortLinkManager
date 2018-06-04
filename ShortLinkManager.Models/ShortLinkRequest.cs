using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLinkManager.Models
{
    public class ShortLinkRequest
    {
        public string Link { get; set; }
        public string GuestGuid { get; set; }
    }
}
