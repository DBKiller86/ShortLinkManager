using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ShortLinkManager.Data.Models
{
    public class ShortLink
    {
        [Key]
        public String Key { get; set; }

        [Required]
        public String Url { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
