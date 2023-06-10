using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostgresTester.Database;

namespace PostgresTester.Pages.Books3
{
    public class IndexModel : PageModel
    {
        private readonly Postgres3Context _generalContext;

        public IndexModel(Postgres3Context generalContext)
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
