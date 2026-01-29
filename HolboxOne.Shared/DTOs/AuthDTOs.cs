namespace HolboxOne.Shared.DTOs;

public class LoginDTO
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class RegisterClientDTO
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class RegisterAdminDTO
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class TokenDTO
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}

public class UserDTO
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserType { get; set; } = null!;
    public string? ProfilePicture { get; set; }
}
