using Microsoft.AspNetCore.Mvc;

namespace CandidateManagement.Web.Controllers;

// ------------------------------------------------------------
// JobController: Placeholder quản lý vị trí tuyển dụng
// ------------------------------------------------------------
public class JobController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Title = "Vị trí tuyển dụng";
        return View();
    }
}

