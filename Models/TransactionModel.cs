namespace ITP_PROJECT.Models
{
    public class TransactionModel
    {
        internal readonly object Timestamp;

        public object CustomerID { get; internal set; }
        public object ProductID { get; internal set; }
        public object Amount { get; internal set; }
        public object TransactionID { get; internal set; }
        public object PaymentMethod { get; internal set; }
    }
}