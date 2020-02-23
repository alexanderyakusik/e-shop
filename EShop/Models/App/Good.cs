using System.ComponentModel.DataAnnotations;

namespace EShop.Models.App
{
    public class Good
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Good must have a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Good must have a price")]
        [Range(0, double.MaxValue, ErrorMessage = "Must be more than zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Must specify amount of goods")]
        [Range(1, int.MaxValue, ErrorMessage = "Cannot be less than one")]
        public int Amount { get; set; }

        public bool InCatalog { get; set; }
    }
}
