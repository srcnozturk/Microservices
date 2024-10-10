using Course.Web.Models;
using IdentityModel.Client;
using Shared.Dtos;
using System.Threading.Tasks;

namespace Course.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signInInput);
        Task<TokenResponse> GetAccesTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
