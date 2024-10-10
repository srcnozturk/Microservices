using System.Threading.Tasks;

namespace Course.Web.Services.Interfaces
{
    public interface IClientCrediantialTokenService
    {
        Task<string> GetToken();
    }
}
