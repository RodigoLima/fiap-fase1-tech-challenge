using System.ComponentModel.DataAnnotations;

public class UserUpdateRequest
{
    [Required]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    public string? OldPassword { get; set; }  // Optional, mas validada no Service

    public string? NewPassword { get; set; }  // Optional

    [Required]
    public int RoleId { get; set; }


}
