using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLTV_TKPM.Data;
using QLTV_TKPM.Models;

namespace QLTV_TKPM.Pages.Phieumuonsachs
{
    public class DeleteModel : PageModel
    {
        private readonly QLTV_TKPM.Data.QLTV_TKPMContext _context;

        public DeleteModel(QLTV_TKPM.Data.QLTV_TKPMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Phieumuonchitiet> Phieumuonchitiets { get; set; }
        [BindProperty]
        public  Phieumuonsach Phieumuonsachs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Phieumuonchitiet == null)
            {
                return NotFound();
            }

            var phieumuonsach = await _context.Phieumuonsach.FirstOrDefaultAsync(m => m.Id == id);

            if (phieumuonsach == null)
            {
                return NotFound();
            }
            else 
            {
                Phieumuonsachs = phieumuonsach;
            }

            var phieumuonchitiet = await _context.Phieumuonchitiet.Where(m => m.Maphieumuonsach == phieumuonsach.Id).ToListAsync();
            if (phieumuonchitiet != null)
            {
                Phieumuonchitiets = phieumuonchitiet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Phieumuonsach == null)
            {
                return NotFound();
            }
            var phieumuonsach = await _context.Phieumuonsach.FindAsync(id);

            if (phieumuonsach != null)
            {
                Phieumuonsachs = phieumuonsach;
                _context.Phieumuonsach.Remove(Phieumuonsachs);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
