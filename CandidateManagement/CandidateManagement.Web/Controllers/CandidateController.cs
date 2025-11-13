using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CandidateManagement.Business.Interfaces;
using CandidateManagement.Data.Entities;
using CandidateManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CandidateManagement.Web.Controllers;

// ------------------------------------------------------------
// CandidateController: Xử lý CRUD ứng viên
// ------------------------------------------------------------
public class CandidateController : Controller
{
    private readonly ICandidateService _candidateService;
    private readonly ILogger<CandidateController> _logger;

    private static readonly string[] StatusOptions = { "New", "Contacted", "Interviewing", "Hired", "Rejected" };

    public CandidateController(ICandidateService candidateService, ILogger<CandidateController> logger)
    {
        _candidateService = candidateService;
        _logger = logger;
    }

    // GET: Candidate
    public async Task<IActionResult> Index()
    {
        var candidates = await _candidateService.GetAllAsync();
        var viewModels = candidates.Select(MapToViewModel);
        return View(viewModels);
    }

    // GET: Candidate/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var candidate = await _candidateService.GetByIdAsync(id);
        if (candidate == null)
        {
            return NotFound();
        }

        return View(MapToViewModel(candidate));
    }

    // GET: Candidate/Create
    public IActionResult Create()
    {
        ViewBag.StatusOptions = StatusOptions;
        return View(new CandidateViewModel());
    }

    // POST: Candidate/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CandidateViewModel model)
    {
        ViewBag.StatusOptions = StatusOptions;

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var candidate = MapToEntity(model);

        Stream? fileStream = null;
        try
        {
            if (model.CvFile != null)
            {
                if (!model.CvFile.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(nameof(model.CvFile), "Chỉ chấp nhận file .pdf");
                    return View(model);
                }

                fileStream = model.CvFile.OpenReadStream();
            }

            await _candidateService.CreateAsync(candidate, fileStream, model.CvFile?.FileName);
            TempData["Success"] = "Thêm ứng viên thành công";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi tạo ứng viên");
            ModelState.AddModelError(string.Empty, "Không thể tạo ứng viên. Vui lòng thử lại.");
            return View(model);
        }
        finally
        {
            fileStream?.Dispose();
        }
    }

    // GET: Candidate/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.StatusOptions = StatusOptions;

        var candidate = await _candidateService.GetByIdAsync(id);
        if (candidate == null)
        {
            return NotFound();
        }

        return View(MapToViewModel(candidate));
    }

    // POST: Candidate/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CandidateViewModel model)
    {
        ViewBag.StatusOptions = StatusOptions;

        if (id != model.CandidateId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var candidate = MapToEntity(model);

        Stream? fileStream = null;
        try
        {
            if (model.CvFile != null)
            {
                if (!model.CvFile.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(nameof(model.CvFile), "Chỉ chấp nhận file .pdf");
                    return View(model);
                }

                fileStream = model.CvFile.OpenReadStream();
            }

            await _candidateService.UpdateAsync(candidate, fileStream, model.CvFile?.FileName);
            TempData["Success"] = "Cập nhật ứng viên thành công";
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi cập nhật ứng viên");
            ModelState.AddModelError(string.Empty, "Không thể cập nhật ứng viên. Vui lòng thử lại.");
            return View(model);
        }
        finally
        {
            fileStream?.Dispose();
        }
    }

    // GET: Candidate/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var candidate = await _candidateService.GetByIdAsync(id);
        if (candidate == null)
        {
            return NotFound();
        }

        return View(MapToViewModel(candidate));
    }

    // POST: Candidate/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _candidateService.DeleteAsync(id);
            TempData["Success"] = "Đã xoá ứng viên";
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi xoá ứng viên");
            TempData["Error"] = "Không thể xoá ứng viên.";
        }

        return RedirectToAction(nameof(Index));
    }

    private static CandidateViewModel MapToViewModel(Candidate candidate)
    {
        return new CandidateViewModel
        {
            CandidateId = candidate.CandidateId,
            FullName = candidate.FullName,
            Email = candidate.Email,
            Phone = candidate.Phone,
            Skills = candidate.Skills,
            Experience = candidate.Experience,
            CvUrl = candidate.CvUrl,
            Status = candidate.Status,
            CreatedAt = candidate.CreatedAt
        };
    }

    private static Candidate MapToEntity(CandidateViewModel model)
    {
        return new Candidate
        {
            CandidateId = model.CandidateId,
            FullName = model.FullName,
            Email = model.Email,
            Phone = model.Phone,
            Skills = model.Skills,
            Experience = model.Experience,
            CvUrl = model.CvUrl,
            Status = model.Status,
            CreatedAt = model.CreatedAt == default ? DateTime.UtcNow : model.CreatedAt
        };
    }
}

