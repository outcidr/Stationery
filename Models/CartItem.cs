namespace Stationery.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public string UserId { get; set; } // Link to ASP.NET Identity User
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}
