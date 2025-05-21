using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Field is required")]
        [EmailAddress(ErrorMessage = "Enter Valid Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }
    }
}
