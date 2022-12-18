using FreeCourse.Web.Models;
using Microsoft.Extensions.Options;

namespace FreeCourse.Web.Helpers
{
    public class PhotoHelper
    {
        private readonly ServiceAppSettings _serviceApiSettings;

        public PhotoHelper(IOptions<ServiceAppSettings> settings)
        {
            _serviceApiSettings = settings.Value;
        }

        public string GetPhotoStockUrl(string photoUrl)
        {
            return $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
        }

    }
}
