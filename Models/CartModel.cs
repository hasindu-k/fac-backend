namespace Product.Models
{
    public class CartModel
    {
        public int cId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }   
        public int quantity { get; set; }

    }
}
