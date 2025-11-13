using System.ComponentModel.DataAnnotations;

namespace CandidateManagement.Data.Entities;

// ------------------------------------------------------------
// User: Tài khoản hệ thống
// ------------------------------------------------------------
public class User
{
    public int UserId { get; set; }

    [Required, StringLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public int RoleId { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation
    public Role? Role { get; set; }
}

