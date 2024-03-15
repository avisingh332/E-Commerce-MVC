using E_Commerce.DataAccess.Repository.IRepository;
using E_Commerce_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce_Application.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var productList = _unitOfWork.Product.GetALL(includeProperties: "Category").ToList();
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            var product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category");
            return View(product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}