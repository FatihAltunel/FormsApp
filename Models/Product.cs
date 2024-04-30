using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Product{
        [Display(Name ="Product ID")]
        public int ProductId { get; set;}

        [Display(Name ="Product Name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Display(Name ="Product Image")]
        public string Image { get; set; } = string.Empty;

        [Display(Name ="Product Price")]
        [Required]
        [Range(0,100000)]
        public decimal Price {get; set;}

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int? CategoryId {get; set;}

    };
}