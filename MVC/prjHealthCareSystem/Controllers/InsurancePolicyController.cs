using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjHealthCareSystem.Data;
using prjHealthCareSystem.Models;

namespace prjHealthCareSystem.Controllers
{
    public class InsurancePolicyController : Controller
    {
        //Adding the data class
        private readonly ApplicationDbContext _context;
        public InsurancePolicyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? startTime, DateTime? endDateTime, string status)
        {
            var search = _context.InsurancePolicies
                .Include(i => i.Patient).AsQueryable();
            
            //Search date
            if (startTime.HasValue && endDateTime.HasValue)
            {
                search = search.Where(p =>
                    p.StartDate >= startTime && p.EndDate <= endDateTime);
            }
            //Search Status
            if (!string.IsNullOrEmpty(status))
            {
                search = search.Where(p => p.Status == status);
            }

            return View(await search.ToListAsync());

        }

        //Create method
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList
                (_context.Patients, "Id", "Name");
            return View();
        }
    }
}