using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace SmartCart.BLL.Services
{
    public interface ITokenService
    {
        Task<string> GetToken(AppUser user);
    }
}
