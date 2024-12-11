using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IServices
{
    public interface IAccountsService
    {
        Task Register(RegisterModel model);
        Task Login(LoginModel model);
        Task Logout();
    }
}
