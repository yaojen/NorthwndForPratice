using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwnd.Models.Repository;
using Northwnd.Models.Interface;
namespace Northwnd.Service.TestService
{
    public class GenericService<T> : IService<T> where T : class
    {
        IUnitOfWork _EFUnitOfWork;
        public GenericService(IUnitOfWork uow)
        {
            this._EFUnitOfWork = uow;
        }
        public void Create()
        {
            _EFUnitOfWork.Repository<T>.Create();
        }

        public void Delete() { }

        public List<T> GetList() { return new List<T>(); }

        public T GetInfo() { return new T(); }

        public void Update() { }
    }
}
