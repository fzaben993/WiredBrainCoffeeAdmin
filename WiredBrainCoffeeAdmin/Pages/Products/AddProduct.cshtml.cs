using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private WiredContext _wiredContext;

        public AddProductModel(WiredContext context)
        {
            _wiredContext = context;
        }

        [BindProperty]
        public Product NewProduct { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // save product to database
                _wiredContext.Products.Add(NewProduct);
                var changes = _wiredContext.SaveChanges();

                return RedirectToPage("ViewAllProducts");
            }

            return Page();
        }
    }
}
