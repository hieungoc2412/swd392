using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CandidateManagement.Data.Entities;

// ------------------------------------------------------------
// Role: Vai trò người dùng
// ------------------------------------------------------------
public class Role
{
    public int RoleId { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}

