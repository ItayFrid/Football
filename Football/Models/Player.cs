using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Football.Models
{
    public class Player
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public int rating { get; set; }
        [Required]
        [Key]
        public int number { get; set; }
        [Required]
        public string position { get; set; }

    }
}