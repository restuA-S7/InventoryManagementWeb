using HangFire.Models;
using Microsoft.EntityFrameworkCore;

namespace HangFire.Service
{
    public class TransactionServices
    {
        private readonly InventoryDbContext _db;

        public TransactionServices(InventoryDbContext db)
        {
            _db = db;
        }

        public async Task DailySummaryService()
        {
            var products = await _db.Products
                .Select(p => new
                {
                    p.Name,
                    p.StockLevel
                })
                .ToListAsync();

            var summary = string.Join("\n", products.Select(p => $"Product: {p.Name}, Stock Level: {p.StockLevel}"));
            Console.WriteLine("Sending Daily Summary Email:\n" + summary);
        }

    }
}
