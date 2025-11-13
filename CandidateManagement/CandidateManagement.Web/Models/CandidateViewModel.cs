using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CandidateManagement.Web.Models;

// ------------------------------------------------------------
// CandidateViewModel: Dùng cho View CRUD
// ------------------------------------------------------------
public class CandidateViewModel
{
    public int CandidateId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    [StringLength(150)]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [StringLength(20)]
    [Display(Name = "Số điện thoại")]
    public string? Phone { get; set; }

    [Display(Name = "Kỹ năng")]
    [StringLength(500)]
    public string? Skills { get; set; }

    [Range(0, 50, ErrorMessage = "Kinh nghiệm phải từ 0-50 năm")]
    [Display(Name = "Số năm kinh nghiệm")]
    public int Experience { get; set; }

    [Display(Name = "Link CV")]
    public string? CvUrl { get; set; }

    [Display(Name = "Trạng thái")]
    [StringLength(50)]
    public string Status { get; set; } = "New";

    [Display(Name = "Ngày tạo")]
    public DateTime CreatedAt { get; set; }

    public IFormFile? CvFile { get; set; }
}

