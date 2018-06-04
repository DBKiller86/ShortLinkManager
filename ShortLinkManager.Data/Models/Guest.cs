using System;
using System.ComponentModel.DataAnnotations;

namespace ShortLinkManager.Data.Models
{
    public class Guest
    {
        [Key]
        public string GuestGuid { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
