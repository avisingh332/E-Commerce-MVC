using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce.Models.Models;
using E_Commerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Data;

namespace E_Commerce_Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;   
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetALL().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetALL().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ProductVM productVm = new ProductVM
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            productVm.CategoryList = CategoryList;
            return View(productVm);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Added Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                //if model state is not valid we want to reload on the same page but 
                //for that also we need to send the category list to the page 
                IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetALL().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                productVM.CategoryList = CategoryList;
                return View(productVM);
            }
            
        }

        public IActionResult Edit(int? id)
        {
            if(id==null|| id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if(productFromDb == null)
            {
                return NotFound();  
            }
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetALL().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ProductVM productVM = new ProductVM
            {
                Product = productFromDb,
                CategoryList = categoryList
            };
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Updated Successfully ";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetALL().Select(u => new SelectListItem
                {
                    Text = u.Name, 
                    Value= u.Id.ToString()
                });

                return View(productVM);
            }
            
        }
        public IActionResult Delete(int? id)
        {
            if(id==null|| id == 0)
            {
                 return NotFound();  
            }
            Product? productFromDb= _unitOfWork.Product.Get(u=>u.Id == id); 
            if (productFromDb == null)
            {
                TempData["error"] = "Product Not Found";
                return NotFound();
            }
            ProductVM productVM = new ProductVM
            {
                Product = productFromDb,
                CategoryList = _unitOfWork.Category.GetALL().Select(u=>new SelectListItem
                {
                    Text=u.Name,
                    Value = u.Id.ToString()   
                })
            };
            return View(productVM); 
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                TempData["error"] = "Product Not Found";
                return NotFound();
            }
            _unitOfWork.Product.Remove(productFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");    
        }
    }
}
