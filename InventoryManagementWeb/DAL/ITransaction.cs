using InventoryManagementWeb.Models;
using InventoryManagementWeb.ViewModels;

namespace InventoryManagementWeb.DAL
{
    public interface ITransaction:ICrud<Transaction>
    {
        IEnumerable<TransactionViewModel> GetTransactions();
        TransactionViewModel GetProductByIdTransaction(int id);
        IEnumerable<TransactionViewModel> GetTransactionByProductName(string name);
    }
}
