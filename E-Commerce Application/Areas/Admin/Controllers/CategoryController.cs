    using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.Models.Models;
using E_Commerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerce_Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles=$"{SD.Role_Admin},{SD.Role_Customer}")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = (List<Category>)_unitOfWork.Category.GetALL();
            return View(objCategoryList);

        }
        public IActionResult Create()
        {
            return View();
        }
        // Action meathod to handle the post request for creating category 
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);

            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Category obj) 
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");   
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]

        //public IActionResult DeletePOST(Category? obj) This can also be the case 
        // it Does matching on the basis of the name of properties in Category
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            //return RedirectToAction("Index",controllerName: "Category");
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
