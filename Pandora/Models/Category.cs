using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pandora.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Navigation property 1 to many
        [ValidateNever]
        public List<Product> Products { get; set; }
    }
}
