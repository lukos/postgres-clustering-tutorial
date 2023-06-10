using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostgresTester.Database;

namespace PostgresTester.Pages.Books1
{
    public class EditModel : PageModel
    {
        private readonly Postgres1Context _generalContext;

        public EditModel(Postgres1Context generalContext)
        {
            _generalContext = generalContext;
        }

        [BindProperty]
        public Book Book { get; set; }


        public void OnGet()
        {
        }

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _generalContext.Attach(Book).State = EntityState.Modified;

            try
            {
                await _generalContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return _generalContext.Books.Any(e => e.id == id);
        }
    }
}
