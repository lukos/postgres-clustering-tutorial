using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostgresTester.Database;

namespace PostgresTester.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly ReadonlyContext readonlyContext;

        public IndexModel(ReadonlyContext readonlyContext)
        {
            this.readonlyContext = readonlyContext;
        }

        public IList<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await readonlyContext.Books.ToListAsync();
        }
    }
}
