using System.ComponentModel.DataAnnotations;

namespace CS.API.ViewModels
{
    public class CategoryDto
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
    }
}
