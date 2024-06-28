using InventoryManagementWeb.DAL;
using InventoryManagementWeb.Models;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                var result = _productEF.Add(product);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Product not added";
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var result = _productEF.GetById(id);
            return View(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int ProductId)
        {
            try
            {
                _productEF.Delete(ProductId);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Product not deleted";
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            var result = _productEF.GetById(id);
            return View(result);
        }

        public IActionResult Edit(int id)
        {
            var result = _productEF.GetById(id);
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            try
            {
                var result = _productEF.Update(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Product not updated";
                return View();
            }
        }
    }
}
