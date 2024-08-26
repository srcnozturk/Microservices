using Course.Web.Models;
using Course.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace Course.Web.Services
{
    public class UserService : IUserService
    {
        public Task<UserViewModel> GetUser()
        {
            throw new System.NotImplementedException();
        }
    }
}
