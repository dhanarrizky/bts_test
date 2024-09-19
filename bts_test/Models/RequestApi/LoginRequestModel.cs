using System.ComponentModel.DataAnnotations;

namespace bts_test.Models;
public class LoginRequestModel
{
    [Required]
    public string Username { get; set; } = null!; 
    [Required]
    public string Password { get; set; } = null!; 
}
