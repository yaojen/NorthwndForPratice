using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwnd.Models.Interface;
using Northwnd.Service.Interface;
using Northwnd.Models;

namespace Northwnd.Service.Service
{
    public class ProductService : GenericService<Product>, IProductService
    {
        public ProductService(IUnitOfWork uow) : base(uow)
        {

        }
    }
}
