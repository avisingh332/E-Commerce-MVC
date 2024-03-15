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
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetALL(includeProperties:"Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetALL().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ProductVM productVM = new ProductVM
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if (id == null || id == 0)
            {
               
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties:"Category");
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string rootPath = _webHostEnvironment.WebRootPath;
                    //generate a new file name 
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var productPath = Path.Combine(rootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // Image already exist and we want to update that image 
                        // so we need to delete the old image

                        var oldImagePath = Path.Combine(rootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    //now create a file stream and use it 
                    using( var fileSteam = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileSteam);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                   
                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
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

        #region API CALLs
        [HttpGet]
        public  IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetALL(includeProperties: "Category").ToList();
            return Json(new {data = objProductList});
        }
        #endregion
    }
}
