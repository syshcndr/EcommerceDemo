using EcommerceDemoWeb.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDemoWeb.Areas.Admin.Models
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

        
        //public int CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        //[ValidateNever]
        //public Category Category { get; set; }
        [Required]
        public string CategoryName { get; set; }

        
        //public int SellerId { get; set; }
        //[ValidateNever]
        //public Seller Seller { get; set; }
        //[Required]
        public string SellerName { get; set; }
    }
}