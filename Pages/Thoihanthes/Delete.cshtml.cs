﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLTV_TKPM.Data;
using QLTV_TKPM.Models;

namespace QLTV_TKPM.Pages.Thoihanthes
{
    public class DeleteModel : PageModel
    {
        private readonly QLTV_TKPM.Data.QLTV_TKPMContext _context;

        public DeleteModel(QLTV_TKPM.Data.QLTV_TKPMContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Thoihanthe Thoihanthe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Thoihanthe == null)
            {
                return NotFound();
            }

            var thoihanthe = await _context.Thoihanthe.FirstOrDefaultAsync(m => m.Id == id);

            if (thoihanthe == null)
            {
                return NotFound();
            }
            else 
            {
                Thoihanthe = thoihanthe;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Thoihanthe == null)
            {
                return NotFound();
            }
            var thoihanthe = await _context.Thoihanthe.FindAsync(id);

            if (thoihanthe != null)
            {
                Thoihanthe = thoihanthe;
                _context.Thoihanthe.Remove(Thoihanthe);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
