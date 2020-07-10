using System.ComponentModel.DataAnnotations;

namespace CS.API.ViewModels
{
    public class ProductDto
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public double NetPrice { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public string Image { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
        public long CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
