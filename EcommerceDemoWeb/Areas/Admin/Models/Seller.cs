using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemoWeb.Controllers
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Seller")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
    }
}
