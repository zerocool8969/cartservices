using System.Collections.Generic;

namespace CS.API.ViewModels
{
    public class CartDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int NewQuantity { get; set; }
        public int TotalAmount { get; set; }
        public int OldQuantity { get; set; }
        public string Image { get; set; }
    }

    public class CheckOutDto
    {
        public string Id { get; set; }
        public string ZipCode { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public List<CartDto> Cart { get; set; }
    }
}
