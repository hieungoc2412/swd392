using Microsoft.AspNetCore.Mvc;

namespace CandidateManagement.Web.Controllers;

// ------------------------------------------------------------
// ApplicationController: Placeholder quản lý hồ sơ ứng tuyển
// ------------------------------------------------------------
public class ApplicationController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Title = "Hồ sơ ứng tuyển";
        return View();
    }
}

