using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlaySpace.Models
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите ваш email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}