namespace bts_test.Models;
public class UserModel
{
    public int RegistrationID { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
