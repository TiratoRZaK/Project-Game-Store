using System.ComponentModel.DataAnnotations;

namespace Business_Logic_Layer.Models
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