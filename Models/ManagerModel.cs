namespace Product.Models
{
    public class ManagerModel
    {
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public required string password { get; set; }
        public required string UserType { get; set; }       
    }
}
