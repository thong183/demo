using Acme.News_Website.BlogImages;
using Acme.News_Website.Blogs;
using Acme.News_Website.Images;
using Acme.News_Website.Trackings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace Acme.News_Website.EntityFrameworkCore.Repository
{
    public class BlogRepository : EfCoreRepository<News_WebsiteDbContext, Blog, Guid>, IBlogRepository
    {
        private readonly IRepository<BlogImage, Guid> _blogRepo;
        public BlogRepository(IDbContextProvider<News_WebsiteDbContext> dbContextProvider, IRepository<BlogImage, Guid> blogRepo) : base(dbContextProvider)
        {
            _blogRepo = blogRepo;
        }
        // Get list blog by category name contain child's blogs
        [Obsolete]
        public List<Blog> GetListByCategoryName(string CateUrl, int skip, int? top)
        {

            var cate = DbContext.Categories.FirstOrDefault(x => x.CategoryUrl == CateUrl);

            if (cate == null) throw new Exception();
            List<Guid> lstCategoriesPar = new List<Guid>();
            List<Guid> lstCategoriesPar2 = new List<Guid>();
            lstCategoriesPar.Add(cate.Id);
            lstCategoriesPar2.Add(cate.Id);
            while (true)
            {
                var lst2 = DbContext.Categories.Where(x => lstCategoriesPar.Contains(x.IdParent.Value)).Select(x => x.Id).ToList();
                if (lst2.Count == 0 || lst2 == null)
                {
                    break;
                }
                else
                {
                    foreach (var item in lst2)
                    {
                        lstCategoriesPar.RemoveRange(0, lstCategoriesPar.Count());
                        lstCategoriesPar.Add(item);
                        lstCategoriesPar2.Add(item);
                    }
                }
            }
            var _result = new List<Blog>();
            if(top == null)
            {
                 _result = DbContext.Blogs.Where(x => x.State == BlogState.Accepted &&
                                                lstCategoriesPar2.Contains(x.IdCategory))
                                                .OrderByDescending(x => x.CreationTime).Skip(skip)
                                                .ToList();
            }
            else
            {
                _result = DbContext.Blogs.Where(x => x.State == BlogState.Accepted &&
                                                lstCategoriesPar2.Contains(x.IdCategory))
                                                .OrderByDescending(x => x.CreationTime).Skip(skip).Take((int)top+1)
                                                .ToList();
            } 
            
            return (_result);
        }

        [Obsolete]
        public List<Blog> GetNewestBlog(int top)
        {

            var _res = DbContext.Blogs.Where(x => x.State == BlogState.Accepted).OrderByDescending(x => x.CreateDate).Take(top).ToList();
            return _res;
        }

        [Obsolete]
        public List<Blog> GetListPopularBlogs(int top, int skip, bool des, Sorting date = Sorting.Day)
        {
            var blogs = new List<Blog>();
            var tracking = new List<Guid>();
            if (des)
            {
                tracking = DbContext.Trackings.OrderByDescending(x => x.SeenCount).Skip(skip).Take(top).Select(x => x.IdBlog).ToList();
            }
            else
            {
                tracking = DbContext.Trackings.OrderBy(x => x.SeenCount).Skip(skip).Take(top).Select(x => x.IdBlog).ToList();
            }
            foreach (var item in tracking)
            {
                switch (date)
                {
                    case Sorting.Day:
                        var blog = DbContext.Blogs.SingleOrDefault(x => x.Id == item 
                                            && x.CreateDate.Day == DateTime.UtcNow.Day 
                                            && x.CreateDate.Month == DateTime.UtcNow.Month
                                            && x.CreateDate.Year == DateTime.UtcNow.Year
                                            && x.State == BlogState.Accepted);
                        if (blog != null)
                        {
                            blogs.Add(blog);
                        }
                        break;
                    case Sorting.Month:
                        blog = DbContext.Blogs.SingleOrDefault(x => x.Id == item && x.CreateDate.Month == DateTime.UtcNow.Month
                                            && x.CreateDate.Year == DateTime.UtcNow.Year
                                            && x.State == BlogState.Accepted);
                        if (blog != null)
                        {
                            blogs.Add(blog);
                        }
                        break;
                    case Sorting.Year:
                        blog = DbContext.Blogs.SingleOrDefault(x => x.Id == item && x.CreateDate.Year == DateTime.UtcNow.Year && x.State == BlogState.Accepted);
                        if (blog != null)
                        {
                            blogs.Add(blog);
                        }
                        break;
                }
            }
            return blogs;
        }

        [Obsolete]
        public List<Blog> GetTopLikeBlogs(int top)
        {
            return DbContext.Blogs.Where(x => x.State == BlogState.Accepted).OrderByDescending(x => x.CountLike).Take(top).ToList();
        }

        [Obsolete]
        public List<Blog> GetAllRepository(BlogState state = BlogState.Accepted)
        {
            return DbContext.Blogs.Where(x => x.State == state).ToList();
        }

        [Obsolete]
        public void AddTagToBlog(Guid idBlog, Guid idTag)
        {
            var _res = DbContext.Blogs.Find(idBlog).Tags;
            var tag = DbContext.Tags.Find(idTag);
            if (tag == null) throw new Exception("Tag is not Exist !");
            else
            {
                _res.Add(tag);
            }
        }
        [Obsolete]
        public async Task CountSeen(Guid idBlog)
        {
            var trackingRes = await DbContext.Trackings.AnyAsync(x => x.IdBlog == idBlog);
            if (!trackingRes)
            {
               await this.AddTracking(idBlog);
            }
            DbContext.Trackings.SingleOrDefaultAsync(x => x.IdBlog == idBlog).Result.SeenCount += 1;
            await DbContext.SaveChangesAsync();
        }

        [Obsolete]
        public List<string> GetImagesFromBlog(Guid idBlog)
        {
            var lstImg = new List<string>();
            if (DbContext.BlogImages.Any(x => x.IdBlog == idBlog))
            {
                var res = DbContext.BlogImages.Where(x => x.IdBlog == idBlog).Select(x => x.IdImage).ToList();
                foreach (var item in res)
                {
                    lstImg.Add(DbContext.Images.Find(item).ImageUrl);
                }
            }
            if (lstImg.Count == 0) lstImg.Add("");
            return lstImg;
        }

        [Obsolete]
        public string GetTitleImage(Guid id)
        {
            var img = "#";
            if (DbContext.BlogImages.Any(x => x.IdBlog == id))
            {
                var _res = DbContext.BlogImages.Where(x => x.IdBlog == id).ToList();
                foreach (var res in _res)
                {
                    if (res.IsTitle)
                    {
                        img = DbContext.Images.Find(res.IdImage).ImageUrl;
                    }
                }

            }
            return img;
        }

        [Obsolete]
        public void SetTitleImage(Guid idBlog, Guid idImage)
        {
            var oldImg = DbContext.BlogImages.SingleOrDefault(x => x.IdBlog == idBlog && x.IsTitle == true);
            var image = DbContext.BlogImages.SingleOrDefault(x => x.IdImage == idImage);
            if (oldImg == null)
            {
                image.IsTitle = true;
            }
            else
            {
                oldImg.IsTitle = false;
                image.IsTitle = true;
            }
        }

        [Obsolete]
        public List<Blog> GetBlogByUser(string username, BlogState state)
        {
            var blogs = new List<Blog>();
            if(DbContext.Users.Any(x => x.NameUrl.ToLower() == username.ToLower()))
            {
                var idUser = DbContext.Users.SingleOrDefault(x => x.NameUrl.ToLower() == username.ToLower()).Id;
                blogs = DbContext.Blogs.Where(x => x.IdUser == idUser && x.State == state).ToList();
            }
            return blogs;
        }

        [Obsolete]
        public List<Blog> GetNewBlogByTime(Sorting time)
        {
            var blogs = new List<Blog>();
            switch (time)
            {
                case Sorting.Day:
                    blogs = DbContext.Blogs.Where(x => x.CreateDate.Day == DateTime.UtcNow.Day && x.State == BlogState.Accepted)
                                            .ToList();
                    break;
                case Sorting.Month:
                    blogs = DbContext.Blogs.Where(x => x.CreateDate.Month == DateTime.UtcNow.Month && x.State == BlogState.Accepted)
                                            .ToList();
                    break;
                case Sorting.Year:
                    blogs = DbContext.Blogs.Where(x => x.CreateDate.Year == DateTime.UtcNow.Year && x.State == BlogState.Accepted)
                                            .ToList();
                    break;
            }
            return blogs;

        }


        [Obsolete]
        public void AddTagsToBlog(Guid IdTag, Guid IdBlog)
        {
            var _tag = DbContext.Tags.Find(IdTag);
            var _blog = DbContext.Blogs.Find(IdBlog);
            _blog.Tags.Add(_tag);
            //_tag.Blogs.Add(_blog);
        }

        [Obsolete]
        public async Task ChangeBlogState(Guid id, BlogState state)
        {
            var blog = await DbContext.Blogs.FindAsync(id);
            switch (state)
            {
                case BlogState.Saved:
                    blog.State = BlogState.Saved;
                    break;
                case BlogState.Requested:
                    blog.State = BlogState.Requested;
                    break;
                case BlogState.Accepted:
                    blog.State = BlogState.Accepted;
                    break;
                case BlogState.Dismissed:
                    blog.State = BlogState.Dismissed;
                    break;
            }
            await DbContext.SaveChangesAsync();
        }

        [Obsolete]
        public bool ChangeCategory(Guid id, Guid newCate)
        {
            var blog = DbContext.Blogs.Find(id);
            if (DbContext.Categories.Any(x => x.Id == newCate))
            {
                blog.IdCategory = newCate;
                return true;
            }
            else return false;
        }
        [Obsolete]
        public void AddImagesToBlog(Guid idBlog, Guid idImage)
        {
            var blogImage = new BlogImage()
            {
                IdBlog = idBlog,
                IdImage = idImage,
                IsTitle = true
            };
            var img = DbContext.Images.Find(idImage);
            DbContext.Blogs.Include("Images").SingleOrDefault(x => x.Id == idBlog).Images.Add(img);
            _blogRepo.InsertAsync(blogImage);
        }

        [Obsolete]
        public bool DeleteImageFromBlog(Guid idImage)
        {

            var img = DbContext.BlogImages.SingleOrDefault(x => x.IdImage == idImage);
            if (img != null)
            {
                DbContext.BlogImages.Remove(img);
                DbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        [Obsolete]
        public async Task SaveChanges(Guid id, Guid idUser, DateTime createDate)
        {
            DbContext.Blogs.Find(id).CreateDate = createDate;
            DbContext.Blogs.Find(id).IdUser = idUser;
            await DbContext.SaveChangesAsync();
        }

        [Obsolete]
        public bool SendRequest(Guid idUser, Guid idBlog)
        {
            var blog = DbContext.Blogs.Find(idBlog);
            if (blog.IdUser == idUser)
            {
                DbContext.Blogs.Find(idBlog).State = BlogState.Requested;
                return true;
            }
            else
            {
                return false;
            }
        }

        [Obsolete]
        public KeyValuePair<string,string> GetAuthor(Guid id)
        {
            var author = new KeyValuePair<string, string>();
            var AuthorId = DbContext.Blogs.Find(id).IdUser;
            if (DbContext.Users.Any(x => x.Id == AuthorId))
            {
               var AuthorTmp = new KeyValuePair<string,string>(DbContext.Users.Find(AuthorId).Name, DbContext.Users.Find(AuthorId).NameUrl);
                author = AuthorTmp;
            }
            return author;

        }
        [Obsolete]
        public string GetCategoryName(Guid id)
        {
            var CateId = DbContext.Blogs.Find(id).IdCategory;
            if (DbContext.Categories.Any(x => x.Id == CateId))
            {
                return DbContext.Categories.Find(CateId).Name;
            }
            else return "";

        }
        [Obsolete]
        public BlogState GetState(Guid id)
        {
            return DbContext.Blogs.Find(id).State;
        }

        [Obsolete]
        public async Task AddTracking(Guid idBlog)
        {
            var tracking = new Tracking(idBlog);
            await DbContext.Trackings.AddAsync(tracking);
            await DbContext.SaveChangesAsync();
        }

        [Obsolete]
        public async Task<bool> LikePost(Guid idBlog)
        {
            if(await DbContext.Blogs.AnyAsync(x => x.Id == idBlog))
            {
                DbContext.Blogs.FindAsync(idBlog).Result.CountLike += 1;
                return true;
            }else return false;
        }

        [Obsolete]
        public async Task<Blog> GetDetailByTitle(string titleUrl)
        {
            
            var check = await DbContext.Blogs.AnyAsync(x => x.TitleUrl == titleUrl);
            if (check)
            {
                return  DbContext.Blogs.SingleOrDefault(x => x.TitleUrl == titleUrl);
            }
            else
            {
                return null;
            }
        }
        public string UrlFriendly(string s)
        {
            s.ToLower();
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(" ", "-").Replace(":","").Replace(",","").Replace(".","");
        }

        [Obsolete]
        public async Task SaveTitleUrl(Guid id,string url)
        {
            DbContext.Blogs.FindAsync(id).Result.TitleUrl = url;
            await DbContext.SaveChangesAsync();
        }
    }
}
