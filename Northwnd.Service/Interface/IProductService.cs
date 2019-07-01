using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwnd.Models;
using Northwnd.Service.Misc;

namespace Northwnd.Service.Interface
{
    public interface IProductService
    {
        IResult Create(Product instance);

        IResult Update(Product instance);

        IResult Delete(int productID);

        bool IsExists(int productID);

        Product GetByID(int productID);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetByCategory(int categoryID);
    }
}
