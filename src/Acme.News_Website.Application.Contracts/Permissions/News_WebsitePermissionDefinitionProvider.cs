using Acme.News_Website.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.News_Website.Permissions
{
    public class News_WebsitePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var news_WebsiteGroup = context.AddGroup(News_WebsitePermissions.GroupName,L("Permission:NewsWebsite"));

            //Define your own permissions here. Example:
            //myGroup.AddPermission(News_WebsitePermissions.MyPermission1, L("Permission:MyPermission1"));
            var blogsPermission = news_WebsiteGroup.AddPermission(News_WebsitePermissions.Blogs.Default, L("Permission:Blogs"));
            blogsPermission.AddChild(News_WebsitePermissions.Blogs.Create, L("Permission:Blogs.Create"));
            blogsPermission.AddChild(News_WebsitePermissions.Blogs.Edit, L("Permission:Blogs.Edit"));
            blogsPermission.AddChild(News_WebsitePermissions.Blogs.Delete, L("Permission:Blogs.Delete"));

            var CategoriesPermission = news_WebsiteGroup.AddPermission(News_WebsitePermissions.Categories.Default);
            CategoriesPermission.AddChild(News_WebsitePermissions.Categories.Create);
            CategoriesPermission.AddChild(News_WebsitePermissions.Categories.Edit);
            CategoriesPermission.AddChild(News_WebsitePermissions.Categories.Delete);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<News_WebsiteResource>(name);
        }
    }
}
