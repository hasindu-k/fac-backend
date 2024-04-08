namespace ITP_PROJECT.Models
{
    public class FertilizerModel
    {
        public int FId { get; set; } // Fertilizer ID
        public string FName { get; set; } // Fertilizer Name
        public string Description { get; set; } // Description
        public string ApplicationMethod { get; set; } // Application Method
        public int UnitPrice { get; set; } // Price per Unit
        public string MeasurementUnit { get; set; } // Measurement Unit
        public int StockQuantity { get; set; } // Stock Quantity
        public DateTime CreationDate { get; set; } // Creation Date
        public DateTime LastUpdate { get; set; } // Last Update Date
    }
}
