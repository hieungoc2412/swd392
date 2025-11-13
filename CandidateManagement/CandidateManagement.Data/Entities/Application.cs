using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CandidateManagement.Data.Entities;

// ------------------------------------------------------------
// Application: Hồ sơ ứng tuyển
// ------------------------------------------------------------
public class Application
{
    public int ApplicationId { get; set; }

    [Required]
    public int CandidateId { get; set; }

    [Required]
    public int JobPositionId { get; set; }

    [DataType(DataType.Date)]
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

    [StringLength(50)]
    public string Status { get; set; } = "Submitted";

    // Navigation
    public Candidate? Candidate { get; set; }
    public JobPosition? JobPosition { get; set; }
    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}

