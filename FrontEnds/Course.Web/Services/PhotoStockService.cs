using Course.Web.Models.PhotoStocks;
using Course.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Course.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        //private readonly IHtt

        public Task<bool> DeletePhoto(string photoUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<PhotoStockViewModel> UploadPhoto(IFormFile photo)
        {
            throw new System.NotImplementedException();
        }
    }
}
