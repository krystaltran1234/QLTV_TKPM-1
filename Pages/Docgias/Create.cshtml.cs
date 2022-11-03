using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTV_TKPM.Data;
using QLTV_TKPM.Models;

namespace QLTV_TKPM.Pages.Docgias
{
    public class CreateModel : PageModel
    {
        private readonly QLTV_TKPM.Data.QLTV_TKPMContext _context;

        public CreateModel(QLTV_TKPM.Data.QLTV_TKPMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Docgia Docgia { get; set; }

        public IList<Loaidocgia> Loaidocgia { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Docgia = new Docgia();
            Docgia.Ngaysinh = DateTime.Today;
            Docgia.Ngaylapthe = DateTime.Today;
            
            if (_context.Loaidocgia != null)
            {
                Loaidocgia = await _context.Loaidocgia.ToListAsync();
            }
            return Page();
        }
        

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Docgia.Add(Docgia);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
