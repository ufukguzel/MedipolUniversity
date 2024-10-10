using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MedipolUniversity.Data;
using Microsoft.EntityFrameworkCore;
using MedipolUniversity.Models;

namespace MedipolUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly MedipolUniversity.Data.SchoolContext _context;

        public CreateModel(MedipolUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        // public Student? Student { get; set; } = default!; revıze ett'
        [BindProperty]

        public StudentVM StudentVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Add(new Models.Student());
            entry.CurrentValues.SetValues(StudentVM);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }



    }
}
