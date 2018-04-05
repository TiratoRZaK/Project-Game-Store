using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PlaySpace.Models
{
    public class Game
    {
        [HiddenInput(DisplayValue = false)]
        public int GameId { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название игра")]
        public String Name { get; set; }
        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите жанр игры")]
        public string Category { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание игры")]
        public string Discription { get; set; }
        [Display(Name = "Цена (в рублях)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }
        [Display(Name = "Скидка")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное или нулевое значение для скидки")]
        public int Discount { get; set; }
        public byte[] Image { get; set; }
        public byte[] File { get; set; }

    }
}