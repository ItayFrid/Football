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
        [RegularExpression("^(100|[1-9][0-9]?)$", ErrorMessage = "Must be a number between 1-100")]
        public string rating { get; set; }
        [Key]
        [Required]
        [RegularExpression("^([1-9][0-9]?)$", ErrorMessage = "Must be a number between 1-99")]
        public string number { get; set; }
        [Required]
        public string position { get; set; }

    }
}