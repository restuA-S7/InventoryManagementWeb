using InventoryManagementWeb.Models;
using InventoryManagementWeb.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace InventoryManagementWeb.DAL
{
    public class TransactionEF : ITransaction
    {
        private readonly InventoryDbContext _appDbContext;

        public TransactionEF(InventoryDbContext appDbContext)
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
            var results = _appDbContext.Transactions.ToList();
            return results;
        }

        public Transaction GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TransactionViewModel GetProductByIdTransaction(int id)
        {
            var result = from t in _appDbContext.Transactions
                         join p in _appDbContext.Products
                         on t.ProductId equals p.ProductId
                         where t.TransactionId == id
                         select new TransactionViewModel
                         {
                             TransactionId = t.TransactionId,
                             ProductId = t.ProductId,
                             Name = p.Name,
                             TransactionType = t.TransactionType,
                             Quantity = t.Quantity,
                             Date = t.Date
                         };
            return result.FirstOrDefault();
        }

        public IEnumerable<TransactionViewModel> GetTransactionByProductName(string name)
        {
            var result = from t in _appDbContext.Transactions
                         join p in _appDbContext.Products on t.ProductId equals p.ProductId
                         where p.Name.Contains(name)
                         select new TransactionViewModel
                         {
                             TransactionId = t.TransactionId,
                             ProductId = t.ProductId,
                             Name = p.Name,
                             TransactionType = t.TransactionType,
                             Quantity = t.Quantity,
                             Date = t.Date
                         };
            return result.ToList();
        }

        public IEnumerable<TransactionViewModel> GetTransactions()
        {
            var results = from t in _appDbContext.Transactions
                          join p in _appDbContext.Products
                          on t.ProductId equals p.ProductId
                          select new TransactionViewModel
                          {
                              TransactionId = t.TransactionId,
                              ProductId = t.ProductId,
                              Name = p.Name,
                              TransactionType = t.TransactionType,
                              Quantity = t.Quantity,
                              Date = t.Date
                          };
            return results.ToList();
        }

        public Transaction Update(Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
