using System;
using System.ComponentModel.DataAnnotations;

namespace ITP_PROJECT.Models
{
    public class PayModel
    {
        public int TransactionID { get; set; } // nullable int for TransactionID
        public int CustomerID { get; set; } // nullable int for CustomerID
        public int ProductID { get; set; } // nullable int for ProductID
       // public string Description { get; set; }
        public string PaymentMethod { get; set; } // Required property
        public int Amount { get; set; }
        //public DateTime Timestamp { get; set; }

    }
}



/*namespace ITP_PROJECT.Models

{
  public class PayModel
  {
    public int? TransactionID { get; set; } // nullable int for TransactionID
    public int? CustomerID { get; set; } // nullable int for CustomerID
    public int? ProductID { get; set; } // nullable int for ProductID
    public required string PaymentMethod { get; set; } // Required property
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
 
        
        public object? PaymentMethod { get; internal set; }
        public object? PaymentDate { get; internal set; }
        public object? Description { get; internal set; }
    }
}
*/