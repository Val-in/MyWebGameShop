namespace MyWebGameShop.Models;

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; }
    
    public List<User> Users { get; set; }
}