using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Northwnd.Service.Interface;
using Northwnd.Models.Interface;
using Northwnd.Models.Repository;

namespace Northwnd.Service.Service
{

    public class GenericService<T> : IService<T> where T : class
    {
        protected IUnitOfWork db;
        public GenericService(IUnitOfWork uow)
        {
            this.db = uow;
        }



        public List<T> GetList(Expression<Func<T, bool>> wherePredicate)
        {
            return db.Repository<T>().Reads().ToList();
        }

        public T GetInfo(Expression<Func<T, bool>> wherePredicate)
        {
            return db.Repository<T>().Read(wherePredicate);
        }

        public void Create(T entity)
        {
            db.Repository<T>().Create(entity);
        }

        public void Delete(Expression<Func<T, bool>> wherePredicate)
        {
            var entity = db.Repository<T>().Read(wherePredicate);

            if (entity != null)
            {
                db.Repository<T>().Delete(entity);
            }
        }

        public void Update(T entity)
        {
            db.Repository<T>().Update(entity);
        }


        public void SaveChange()
        {
            db.Save();
        }

        
    }
}
