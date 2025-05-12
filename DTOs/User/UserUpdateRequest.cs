using System.ComponentModel.DataAnnotations;

public class UserUpdateRequest
{
  public string? Name { get; set; }
  public string? Email { get; set; }
  public string? OldPassword { get; set; }  // Optional, mas validada no Service
  public string? NewPassword { get; set; }  // Optional
  public int? RoleId { get; set; }
}
