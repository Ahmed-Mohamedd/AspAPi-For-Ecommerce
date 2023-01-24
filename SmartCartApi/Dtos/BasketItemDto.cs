﻿using System.ComponentModel.DataAnnotations;

namespace SmartCart.Api.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Required]
        
        public string Price { get; set; }

        [Required]
        [Range(1,double.MaxValue,ErrorMessage ="Quantity should be at least equals 1")]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Brand { get; set; }
    }
}