using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedipolUniversity.Data;
using MedipolUniversity.Models;

namespace MedipolUniversity.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly MedipolUniversity.Data.SchoolContext _context;

        // Constructor, veritabanı bağlamını (context) alır
        public DetailsModel(MedipolUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        // Öğrenci nesnesini saklar
        public Student Student { get; set; } = default!;

        // Seçilen öğrencinin detaylarını almak için OnGetAsync kullanılır
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Eğer id parametresi null ise NotFound (404) döner
            if (id == null)
            {
                return NotFound();
            }

            // Veritabanından öğrenciyi, kayıtları ve ilgili ders bilgilerini alır
            Student = await _context.Students
               .Include(s => s.Enrollments) // Öğrencinin kayıt olduğu dersler
               .ThenInclude(e => e.Course)  // Ders bilgileri ile ilişkilendirilir
               .AsNoTracking() // Veriyi takip etmez, sadece okunur
               .FirstOrDefaultAsync(m => m.Id == id);  // Öğrenciyi ID ile bulur

            // Eğer öğrenci bulunamazsa NotFound (404) döner
            if (Student == null)
            {
                return NotFound();
            }

            // Öğrenci bilgilerini ve kayıtları detay sayfasına döner
            return Page();
        }
    }
}
