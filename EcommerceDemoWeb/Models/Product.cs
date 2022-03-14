using EcommerceDemoWeb.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDemoWeb.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]
        public string? CategoryName { get; set; }
        [Required]
        public string? SellerName { get; set; }


        
     
    }
}