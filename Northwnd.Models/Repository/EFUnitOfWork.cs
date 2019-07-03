using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwnd.Models.Interface;
using System.Data.Entity;


namespace Northwnd.Models.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private Hashtable _repositories;
        private bool _disposed;

        public EFUnitOfWork(DbContext context)
        {
             this._context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)_repositories[type];
        }
    }
}
