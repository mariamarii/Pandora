using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pandora.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [ValidateNever]
        public List<Product> Products { get; set; }
    }
}
