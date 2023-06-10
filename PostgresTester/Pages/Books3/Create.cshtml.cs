using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostgresTester.Database;

namespace PostgresTester.Pages.Books3
{
    public class CreateModel : PageModel
    {
        private readonly Postgres3Context _generalContext;

        public CreateModel(Postgres3Context generalContext)
        {
            _generalContext = generalContext;
        }

        [BindProperty]
        public Book Book { get; set; }

        public IActionResult OnGet()
        {
            Book = new Book();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _generalContext.Books.Add(Book);
            await _generalContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
