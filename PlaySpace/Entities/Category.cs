using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlaySpace.Entities
{
    public class Category
    {
        public int Id { get ; set;}
        [Display(Name = "Название категориии")]
        [Required(ErrorMessage = "Пожалуйста, введите название котегории")]
        public string CategoryName { get; set; }

        ICollection<Game> Games { get; set; }
    }
}