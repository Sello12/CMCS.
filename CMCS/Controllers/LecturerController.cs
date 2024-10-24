using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace CMCS.Controllers
{
    public class LecturerController : Controller
    {
        private readonly AppDbContext _context;

        public LecturerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SubmitClaim()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim(decimal hoursWorked, decimal hourlyRate, string notes, IFormFile supportingDocument)
        {
            var claim = new Claim
            {
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                Notes = notes,
                Status = "Pending",
                SubmittedDate = DateTime.Now
            };

            if (supportingDocument != null)
            {
                var filePath = Path.Combine("wwwroot/uploads", supportingDocument.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await supportingDocument.CopyToAsync(stream);
                }
                claim.SupportingDocumentPath = "/uploads/" + supportingDocument.FileName;
            }

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return RedirectToAction("ClaimStatus");
        }

        public IActionResult ClaimStatus()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }
    }
}
