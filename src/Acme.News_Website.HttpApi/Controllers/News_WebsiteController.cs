using Acme.News_Website.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.News_Website.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class News_WebsiteController : AbpController
    {
        protected News_WebsiteController()
        {
            LocalizationResource = typeof(News_WebsiteResource);
        }
    }
}