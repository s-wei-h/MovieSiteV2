using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSiteV2.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string year { get; set; }
        [Required]
        public string director { get; set; }
        [Required]
        public string rating { get; set; }
        //default value = false
        public bool edited { get; set; } = false;

        //default value = not available
        public string lent_to { get; set; } = "N/A";

        //default value = not available
        public string notes { get; set; } = "N/A";
    }
}
