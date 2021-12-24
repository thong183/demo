using System;
using System.Collections.Generic;
using System.Text;
using Acme.News_Website.Localization;
using Volo.Abp.Application.Services;

namespace Acme.News_Website
{
    /* Inherit your application services from this class.
     */
    public abstract class News_WebsiteAppService : ApplicationService
    {
        protected News_WebsiteAppService()
        {
            LocalizationResource = typeof(News_WebsiteResource);
        }
    }
}
