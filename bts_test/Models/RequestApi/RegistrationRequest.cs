namespace bts_test.Models;
public class RegistrationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }  

    [Required]
    public string Username { get; set; } 

    [Required]
    [MinLength(8)]
    public string Password { get; set; } 
}
