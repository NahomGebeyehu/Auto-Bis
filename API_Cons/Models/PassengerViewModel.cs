using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_Cons.Models
{
    public class PassengerViewModel
    {
        List<PassengerViewModel> modelList = new List<PassengerViewModel>();
        public string username { get; set; }
        public string password { get; set; }
        [Compare("password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
        public long phone_no { get; set; }
    }
}