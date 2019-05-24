using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlPrinter.Models;

namespace ControlPrinter.Service.Abstract
{
    public interface IUserService
    {
        UserModel GetByName(string userName);
    }
}
