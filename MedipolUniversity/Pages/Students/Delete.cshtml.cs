using MedipolUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MedipolUniversity.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly MedipolUniversity.Data.SchoolContext _context;
        private readonly ILogger<DeleteModel> _logger;

        // Constructor, veritabanı bağlamını ve logger'ı alır
        public DeleteModel(MedipolUniversity.Data.SchoolContext context,
                           ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Silinecek öğrenci objesini ve hata mesajını saklar
        [BindProperty]
        public Student Student { get; set; }
        public string ErrorMessage { get; set; }

        // GET isteği ile öğrenciyi bulur ve silme sayfasına hazırlar
        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            // Geçerli bir ID yoksa 404 Not Found döner
            if (id == null)
            {
                return NotFound();
            }

            // Öğrenci veritabanından bulunur, takip edilmez (AsNoTracking)
            Student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            // Öğrenci bulunamazsa 404 Not Found döner
            if (Student == null)
            {
                return NotFound();
            }

            // Eğer önceki silme işlemi hata verdiyse, hata mesajını gösterir
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }

            return Page();
        }

        // POST isteği ile öğrenci silme işlemi yapılır
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // Geçerli bir ID yoksa 404 Not Found döner
            if (id == null)
            {
                return NotFound();
            }

            // Öğrenci veritabanından ID ile bulunur
            var student = await _context.Students.FindAsync(id);

            // Öğrenci bulunamazsa 404 Not Found döner
            if (student == null)
            {
                return NotFound();
            }

            try
            {
                // Öğrenci veritabanından silinir ve değişiklikler kaydedilir
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index"); // Başarılı silme işlemi sonrası Index sayfasına yönlendirilir
            }
            catch (DbUpdateException ex) // Silme işleminde hata olursa
            {
                _logger.LogError(ex, ErrorMessage); // Hata loglanır

                // Hata sonrası kullanıcı Delete sayfasına hata mesajı ile geri yönlendirilir
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
