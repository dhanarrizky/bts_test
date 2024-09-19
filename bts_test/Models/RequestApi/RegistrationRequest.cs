using System.ComponentModel.DataAnnotations;

namespace bts_test.Models;
public class RegistrationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;  

    [Required]
    public string Username { get; set; } = null!; 

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = null!; 
}
