namespace ITP_PROJECT.Models
{
    public class SupplyRequestModel
    {
        public int RequestId { get; set; } // Unique identifier for the supply request
        public string RequestedBy { get; set; } // Name or identifier of the person who requested the supply
        public DateTime RequestDate { get; set; } // Date and time when the supply request was made
        public string ItemName { get; set; } // Name of the item being requested
        public int Quantity { get; set; } // Quantity of the item being requested
    }
}
