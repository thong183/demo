namespace Acme.News_Website.Permissions
{
    public static class News_WebsitePermissions
    {
        public const string GroupName = "News_Website";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
        public static class Blogs
        {
            public const string Default = GroupName + "Blogs";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
        public static class Categories
        {
            public const string Default = GroupName + ".Categories";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}