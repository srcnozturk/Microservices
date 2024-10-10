using Course.Web.Models;
using Microsoft.Extensions.Options;

namespace Course.Web.Helpers
{
    public class PhotoHelper
    {
        private readonly ServiceApiSettings _serApiSettings;

        public PhotoHelper(IOptions<ServiceApiSettings> serApiSettings)
        {
            _serApiSettings = serApiSettings.Value;
        }
        public string GetPhotoStockUrl(string photoUrl)
        {
            return $"{_serApiSettings.PhotoStockUrl}/photos/{photoUrl}";
        }
    }
}
