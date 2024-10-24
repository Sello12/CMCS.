using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CMCS.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly AppDbContext _context;

        public CoordinatorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult VerifyClaims()
        {
            var pendingClaims = _context.Claims.Where(c => c.Status == "Pending").ToList();
            return View(pendingClaims);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int claimID)
        {
            var claim = _context.Claims.Find(claimID);
            if (claim != null)
            {
                claim.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("VerifyClaims");
        }

        [HttpPost]
        public async Task<IActionResult> RejectClaim(int claimID)
        {
            var claim = _context.Claims.Find(claimID);
            if (claim != null)
            {
                claim.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("VerifyClaims");
        }
    }
}


