using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CS.Core.Entities
{
    public class Category
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(250)]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
