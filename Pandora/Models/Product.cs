using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;



namespace Pandora.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        [Display(Name = "Product name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        [Display(Name = "Product description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a price")]
        [Range(500, 1000000, ErrorMessage = "Price must be between 0 and 1000000")]
        public decimal Price { get; set; }

        public Nullable<decimal> Discount { get; set; }

        [Display(Name = "Available?")]
        public bool ProductAvailable { get; set; }

        [ValidateNever]
        public string ImagePath { get; set; }

        [NotMapped]
        [DisplayName("Image")]
        [ValidateNever]
        public IFormFile ImageFile { get; set; }

        [DisplayName("Category")]
        [Range(1, double.MaxValue, ErrorMessage = "Please select a Category")]
        public int CategoryId { get; set; }

        // Navigation property many to 1
        [ValidateNever]
        public Category Category { get; set; }

        [DisplayName("Material")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a Material")]
        public int MaterialId { get; set; }

        // Navigation property many to 1
        [ValidateNever]
        public Material Material { get; set; }


    }
}
