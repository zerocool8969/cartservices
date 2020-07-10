using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Core.Entities
{
    public class CheckOut
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
