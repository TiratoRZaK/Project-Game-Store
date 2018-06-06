using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Data_Access_Layer.Entities
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