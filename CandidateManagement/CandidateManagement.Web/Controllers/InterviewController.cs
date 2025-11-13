using Microsoft.AspNetCore.Mvc;

namespace CandidateManagement.Web.Controllers;

// ------------------------------------------------------------
// InterviewController: Placeholder quản lý phỏng vấn
// ------------------------------------------------------------
public class InterviewController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Title = "Phỏng vấn";
        return View();
    }
}

