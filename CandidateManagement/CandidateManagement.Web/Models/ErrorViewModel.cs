namespace CandidateManagement.Web.Models;

// ------------------------------------------------------------
// ErrorViewModel: Dùng cho trang lỗi mặc định
// ------------------------------------------------------------
public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

