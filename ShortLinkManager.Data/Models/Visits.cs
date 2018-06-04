using System;
using System.ComponentModel.DataAnnotations;

namespace ShortLinkManager.Data.Models
{
    public class Visits
    {
        [Key]
        public int VisitID { get; set; }

        public string ShortLinkKey { get; set; }

        public string GuestGuid { get; set; }

        public DateTime DateVisited { get; set; }

    }
}
