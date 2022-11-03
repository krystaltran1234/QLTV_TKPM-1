using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTV_TKPM.Data;
using QLTV_TKPM.Models;

namespace QLTV_TKPM.Pages.Phieumuonsachs
{
    public class CreateModel : PageModel
    {
        private readonly QLTV_TKPM.Data.QLTV_TKPMContext _context;

        public CreateModel(QLTV_TKPM.Data.QLTV_TKPMContext context)
        {
            _context = context;
        }
        public Docgia docgia { get; set; }
               

        public int Soluongsachmuon { get; set; }
        
        public IList<Sach> sachs { get; set; }

        [BindProperty]
        public IList<Docgia> docgias { get; set; }

        [Required]
        [BindProperty]
        public string Madocgia { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BindProperty]
        [Display(Name = "Ngày mượn")]
        public DateTime Ngaymuon { get; set; }

        [BindProperty]
        public IList<string> Masach { get; set; }



        public Phieumuonsach Phieumuonsachs { get; set; }


        public IList<Phieumuonchitiet> Phieumuonchitiets { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Ngaymuon = DateTime.Today;
            var Soluongsachmuons = await _context.Soluongsachmuon.ToListAsync();
                
            if (_context.Soluongsachmuon != null)
            {
                if (Soluongsachmuons.Count != 0) 
                {
                    Soluongsachmuon = Soluongsachmuons[0].Soluongsachmuontoida;
                }
                else
                {
                    Soluongsachmuon = 5;
                }
                
            }
            if(_context.Sach != null)
            {
                sachs = await _context.Sach.ToListAsync();
            }
            if (_context.Docgia != null)
            {
                docgias = await _context.Docgia.ToListAsync();
            }
            return Page();
        }     

        public bool checkMasach ()
        {
            int i = 0;
            foreach(var item in Masach)
            {
                if(item != null)
                {
                    i++;
                }
            }    
            if (i>0)
            {
                return true;
            }
            return false;
        }
       
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           
            if(!checkMasach())
            {

                return null;
            }    

              if (!ModelState.IsValid )
              {
                    return Page();
              }
            //Phieumuonsachs.MaDocGia = int.Parse(Phieumuonsachs.Docgias.Hoten.Split('-')[0]);
            Phieumuonsachs = new Phieumuonsach();
            Phieumuonchitiets = new List<Phieumuonchitiet>();
            Phieumuonsachs.MaDocGia = int.Parse(Madocgia.Split('-')[0]);
            Phieumuonsachs.NgayMuon = Ngaymuon;
            _context.Phieumuonsach.Add(Phieumuonsachs);
            await _context.SaveChangesAsync();
            int j = 0;
            int Maphieumuon = Phieumuonsachs.Id;
            Phieumuonchitiet phieumuonchitiet;
            for(int i=0; i<Masach.Count; i++)
            {
                phieumuonchitiet = new Phieumuonchitiet();
                if (Masach[i]!=null)
                {
                    phieumuonchitiet.Maphieumuonsach = Maphieumuon;
                    phieumuonchitiet.MaSach = int.Parse(Masach[i].Split('-')[0]);
                    _context.Phieumuonchitiet.Add(phieumuonchitiet);
                    
                }    
            }
            await _context.SaveChangesAsync();
        

           //_context.Phieumuonchitiet.Add(1);
            

            return RedirectToPage("./Index");
        }
    }
}
