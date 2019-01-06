using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Football.Models;

namespace Football.ViewModels
{
    public class ViewModel
    {
        public List<Player> players { get; set; }
        public List<Staff> staffs { get; set; }
    }
}