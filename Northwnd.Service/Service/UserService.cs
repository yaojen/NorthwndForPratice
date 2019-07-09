using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwnd.Service.Interface;
using Northwnd.Models;
using Northwnd.Models.Interface;

namespace Northwnd.Service.Service
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
