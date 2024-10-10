using MedipolUniversity.Data;
using MedipolUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedipolUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        // Constructor, veritabanı bağlamını (context) alır
        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        // Sıralama durumu için değişkenler
        public string NameSort { get; set; }  // İsim sıralaması (artan/azalan)
        public string DateSort { get; set; }  // Tarih sıralaması (artan/azalan)
        public string CurrentFilter { get; set; }  // Filtreleme durumu (şu an kullanılmıyor)
        public string CurrentSort { get; set; }  // Mevcut sıralama durumu

        // Öğrencilerin listesi
        public IList<Student> Students { get; set; }

        // GET isteği ile sayfaya yüklenecek veriler
        public async Task OnGetAsync(string sortOrder)
        {
            // Sıralama parametrelerini belirler: varsayılan sıralama isim artan, 
            // "name_desc" isim azalan, "Date" tarih artan, "date_desc" tarih azalan
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            // Veritabanındaki öğrenci kayıtlarını sorgular
            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;

            // Sıralama seçeneklerine göre öğrenci listesini sıralar
            switch (sortOrder)
            {
                case "name_desc":  // İsim azalan sırada
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":  // Kayıt tarihine göre artan sırada
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":  // Kayıt tarihine göre azalan sırada
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:  // Varsayılan sıralama: isim artan sırada
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }

            // Öğrencileri veritabanından alır ve listeye yükler (takip edilmeden)
            Students = await studentsIQ.AsNoTracking().ToListAsync();
        }
    }
}
