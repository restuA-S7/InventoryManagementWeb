using InventoryManagementWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementWeb.DAL
{
    public class TransactionEF : ITransaction
    {
        private readonly AppDbContext _appDbContext;
        public TransactionEF(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Transaction Add(Transaction entity)
        {
            _appDbContext.Transactions.Add(entity);
            _appDbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAll()
        {
            var results = _appDbContext.Transactions.Include(p=>p.Product).OrderBy(p=>p.TransactionId).ToList();
            return results;
        }

        public Transaction GetById(int id)
        {
            var result = _appDbContext.Transactions.Find(id);
            if (result == null)
            {
                throw new Exception("Transaction Tidak Ditemukan");
            }
            return result;
        }

        public Transaction Update(Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
