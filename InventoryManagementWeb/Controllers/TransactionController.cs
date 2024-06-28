using InventoryManagementWeb.DAL;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementWeb.Models;

namespace InventoryManagementWeb.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransaction _transactionEF;
        public TransactionController(ITransaction transactionEF)
        {
            _transactionEF = transactionEF;
        }
        public IActionResult Index()
        {
            var results = _transactionEF.GetAll();
            var list = new List<Transaction>();
            foreach (var result in results)
            {
                list.Add(new Transaction
                {
                    TransactionId = result.TransactionId,
                    ProductId = result.ProductId,
                    TransactionType = result.TransactionType,
                    Quantity = result.Quantity,
                    Date = result.Date,
                    Product = new Product
                    {
                        Name = result.Product.Name
                    }
                });
            }
            return View(list);
        }
    }
}
