using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementWeb.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
