using System;
using System.ComponentModel.DataAnnotations;

namespace CandidateManagement.Data.Entities;

// ------------------------------------------------------------
// Interview: Lịch phỏng vấn
// ------------------------------------------------------------
public class Interview
{
    public int InterviewId { get; set; }

    [Required]
    public int ApplicationId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime InterviewDate { get; set; }

    [Required, StringLength(100)]
    public string Interviewer { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Feedback { get; set; }

    [StringLength(50)]
    public string? Result { get; set; }

    // Navigation
    public Application? Application { get; set; }
}

