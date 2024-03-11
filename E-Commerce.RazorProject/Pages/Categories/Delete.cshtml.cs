using E_Commerce.RazorProject.Data;
using E_Commerce.RazorProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce.RazorProject.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                Category = _db.Categories.FirstOrDefault(c => c.Id == id);
            }
        }
        public IActionResult OnPost()
        {
            Category obj = _db.Categories.Find(Category.Id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Removed Category Successfully";
            return RedirectToPage("Index");
            
        }
    }
}
