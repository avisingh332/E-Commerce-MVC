using E_Commerce.RazorProject.Data;
using E_Commerce.RazorProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_Commerce.RazorProject.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        { 
            _db = db;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category Created Successfully";
            return RedirectToPage("Index");
        }
    }
}
