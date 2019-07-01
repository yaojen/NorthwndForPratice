using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwnd.Models.Interface
{
    public interface IUnitOfWork : IDisposable
    {   /// <summary>
        /// 儲存所有異動
        /// </summary>
        void Save();

        /// <summary>
        /// 取得某一個Entity的Repository
        /// 如果沒有取過，會產生一個
        /// 如果有取過，回傳之前的instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> Repository<T>() where T : class;
    }
}
