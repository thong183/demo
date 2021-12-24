using Volo.Abp.Settings;

namespace Acme.News_Website.Settings
{
    public class News_WebsiteSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(News_WebsiteSettings.MySetting1));
        }
    }
}
