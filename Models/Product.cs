using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Product{
        [Display(Name ="Product ID")]
        public int ProductId { get; set;}
        [Display(Name ="Product Name")]
        public string Name { get; set; } = string.Empty;
        [Display(Name ="Product Image")]
        public string Image { get; set; } = string.Empty;
        [Display(Name ="Product Price")]
        public decimal Price {get; set;}

        public bool IsActive { get; set; }
        public int CategoryId {get; set;}

    };
}