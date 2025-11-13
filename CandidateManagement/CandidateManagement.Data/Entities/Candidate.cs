using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CandidateManagement.Data.Entities;

// ------------------------------------------------------------
// Candidate: Thông tin ứng viên
// ------------------------------------------------------------
public class Candidate
{
    public int CandidateId { get; set; }

    [Required, StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(500)]
    public string? Skills { get; set; }

    [Range(0, 50)]
    public int Experience { get; set; }

    [Url]
    public string? CvUrl { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = "New";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}

