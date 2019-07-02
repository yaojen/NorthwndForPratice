using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwnd.Service.TestService
{
    public interface IService<T> where T : class
    {
        //crud
        void Create();

        void Delete();

        List<T> GetList();

        T GetInfo();

        void Update();
    }
}
