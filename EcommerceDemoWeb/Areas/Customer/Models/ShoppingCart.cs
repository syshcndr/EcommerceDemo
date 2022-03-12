using EcommerceDemoWeb.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemoWeb.Areas.Customer.Models
{
    public class ShoppingCart
    {
        public Product Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Please Enter a value between 1 and 1000")]
        public int Count { get; set; }
    }
}
