﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLTV_TKPM.Data;
using QLTV_TKPM.Models;

namespace QLTV_TKPM.Pages.Soluongsachmuons
{
    public class DetailsModel : PageModel
    {
        private readonly QLTV_TKPM.Data.QLTV_TKPMContext _context;

        public DetailsModel(QLTV_TKPM.Data.QLTV_TKPMContext context)
        {
            _context = context;
        }

      public Soluongsachmuon Soluongsachmuon { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Soluongsachmuon == null)
            {
                return NotFound();
            }

            var soluongsachmuon = await _context.Soluongsachmuon.FirstOrDefaultAsync(m => m.Id == id);
            if (soluongsachmuon == null)
            {
                return NotFound();
            }
            else 
            {
                Soluongsachmuon = soluongsachmuon;
            }
            return Page();
        }
    }
}
