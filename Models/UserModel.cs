namespace ITP_PROJECT.Models;

public class UserModel
{
    public required string managerID { get; set; }
    public string managerName { get; set; }
    public string NIC { get; set; }
    public string managerEmail { get; set; }
    public string managerPhone { get; set; }
    public string managerType { get; set; }        
    public required string managerPassword { get; set; }
}

