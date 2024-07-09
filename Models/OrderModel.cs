namespace ITP_PROJECT.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string OrderState { get; set; }
        public DateTime OrderDate { get; set; }
        public int SupplierID { get; set; }
    }
}
