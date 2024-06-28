using InventoryManagementWeb.DAL;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementWeb.Models;
using InventoryManagementWeb.ViewModels;

namespace InventoryManagementWeb.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransaction _transactionEF;
        private readonly IProduct _productEF;
        public TransactionController(ITransaction transactionEF, IProduct productEF)
        {
            _transactionEF = transactionEF;
            _productEF = productEF;
        }
        public IActionResult Index(string name = "")
        {
            IEnumerable<TransactionViewModel> results;
            if(name != "")
            {
                results = _transactionEF.GetTransactionByProductName(name);
            }
            else
            {
                results = _transactionEF.GetTransactions();
            }
            return View(results);

        }

        public IActionResult Create()
        {
            var result = new TransactionViewModel
            {
                Products = _productEF.GetAll().ToList()
            };

            return View(result);
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            
            try
            {
                var result = _transactionEF.Add(transaction);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return View(transaction);
            }
        }

        public IActionResult Detail(int id)
        {
            var results = _transactionEF.GetProductByIdTransaction(id);
            return View(results);
        }
    }
}
