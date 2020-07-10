using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Core.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CheckOutId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int NewQuantity { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        [Required]
        public int OldQuantity { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
