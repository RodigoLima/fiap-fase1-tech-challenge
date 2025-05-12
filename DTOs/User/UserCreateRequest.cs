using System.ComponentModel.DataAnnotations;

public class UserCreateRequest
{
    [Required, MinLength(6), MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, MinLength(8)]
    public string Password { get; set; } = string.Empty;
    [Required]
    public int RoleId { get; set; }
}
