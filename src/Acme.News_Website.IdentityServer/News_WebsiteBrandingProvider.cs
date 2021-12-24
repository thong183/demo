using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Acme.News_Website
{
    [Dependency(ReplaceServices = true)]
    public class News_WebsiteBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "News_Website";
    }
}
