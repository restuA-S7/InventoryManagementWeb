using InventoryManagementWeb.Models;

namespace InventoryManagementWeb.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        public int? ProductId { get; set; }
        public string Name { get; set; }

        public bool? TransactionType { get; set; }

        public int? Quantity { get; set; }

        public DateTime? Date { get; set; }

        public List<Product>? Products { get; set; }
        public Transaction Transaction { get; set; }
    }
}
