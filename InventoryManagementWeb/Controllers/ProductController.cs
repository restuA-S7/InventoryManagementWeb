using InventoryManagementWeb.DAL;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _productEF;
        public ProductController(IProduct productEF)
        {
            _productEF = productEF;
        }
        public IActionResult Index()
        {
            var result = _productEF.GetAll();
            return View(result);
        }
    }
}
