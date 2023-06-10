using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostgresTester.Database;

namespace PostgresTester.Pages.Books2
{
    public class DeleteModel : PageModel
    {
        private readonly Postgres2Context _generalContext;

        public DeleteModel(Postgres2Context generalContext)
        {
            _generalContext = generalContext;
        }

        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _generalContext.Books.FirstOrDefaultAsync(m => m.id == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _generalContext.Books.FindAsync(id);

            if (Book != null)
            {
                _generalContext.Books.Remove(Book);
                await _generalContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
