using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CandidateManagement.Data.Entities;

// ------------------------------------------------------------
// JobPosition: Vị trí tuyển dụng
// ------------------------------------------------------------
public class JobPosition
{
    public int JobPositionId { get; set; }

    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Department { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? Requirements { get; set; }

    // Navigation
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}

