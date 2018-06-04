using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortLinkManager.Web.Models
{
    public class ShortLinkRequest
    {
        public string Link { get; set; }
        public string GuestGuid { get; set; }
    }
}