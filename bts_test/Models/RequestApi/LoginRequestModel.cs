namespace bts_test.Models;
public class LoginRequestModel
{
    [Required]
    public string Username { get; set; } 
    [Required]
    public string Password { get; set; } 
}
