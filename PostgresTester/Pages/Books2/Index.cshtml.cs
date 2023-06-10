using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostgresTester.Database;

namespace PostgresTester.Pages.Books2
{
    public class IndexModel : PageModel
    {
        private readonly Postgres2Context _generalContext;

        public IndexModel(Postgres2Context generalContext)
        {
            _generalContext = generalContext;
        }

        public IList<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _generalContext.Books.ToListAsync();
        }
    }
}
