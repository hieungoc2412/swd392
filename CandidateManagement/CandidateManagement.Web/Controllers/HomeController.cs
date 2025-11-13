using System.Diagnostics;
using CandidateManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CandidateManagement.Web.Controllers;

// ------------------------------------------------------------
// HomeController: Controller mặc định
// ------------------------------------------------------------
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Candidate");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

