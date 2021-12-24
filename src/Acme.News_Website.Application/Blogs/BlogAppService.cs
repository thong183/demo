using Acme.News_Website.BlogReviews;
using Acme.News_Website.Comments;
using Acme.News_Website.Images;
using Acme.News_Website.Tags;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.News_Website.Blogs
{
    public class BlogAppService :
        CrudAppService<
            Blog,
            BlogDTO,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBlogDTO>,
        IBlogAppService

    {
        private readonly IBlogRepository _blog;
        private readonly IBlogReviewRepository _review;
        private readonly ITagRepository _tag;
        private readonly ICommentRepository _comment;
        public BlogAppService(IRepository<Blog, Guid> repository, IBlogRepository blog, IBlogReviewRepository review, ITagRepository tag, ICommentRepository comment)
            : base(repository)
        {
            _blog = blog;
            _review = review;
            _tag = tag;
            _comment = comment;
        }
        [Authorize]
        public override async Task<BlogDTO> CreateAsync([FromForm] CreateUpdateBlogDTO input)
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            bool flag = true;
            foreach (var name in input.TagName)
            {
                if (_tag.IsExistedTag(name) == false)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                var _res = await base.CreateAsync(input);
                _res.State = BlogState.Saved;
                _res.IdUser = Guid.Parse(currentUser);
                _res.CreateDate = DateTime.UtcNow;
                _res.TitleUrl = _blog.UrlFriendly(_res.Title)+"-"+ DateTime.Now.ToString().Replace("-", "").Replace(" ", "").Replace(":", "").Replace(".", "").Replace("/","").Replace("AM","").Replace("PM","");
                await _blog.SaveTitleUrl(_res.Id, _res.TitleUrl);
                //var image = _image.UploadImages(input.Images,_res.Result.Id);
                _blog.AddImagesToBlog(_res.Id, input.ImageTitleId);
                //_blog.SetTitleImage(_res.Id,input.ImageTitleId);


                if (_tag.AddTagsToBlog(input.TagName, _res.Id))
                {
                    foreach (var tag in input.TagName)
                    {
                        _tag.AddBlogToTag(tag, _res.Id);
                    }
                    await _blog.SaveChanges(_res.Id, _res.IdUser, _res.CreateDate);
                    return _res;
                }
                else throw new Exception("Add Tag Fail");
            }
            else throw new Exception("Tag is not exist ! Please try another or add new Tag");
        }
        public List<BlogDTO> GetListByCategoryName(string cateName,int skip, int top)
        {
            var _res = _blog.GetListByCategoryName(cateName,skip,top);
            var result = ObjectMapper.Map<List<Blog>, List<BlogDTO>>(_res);
            foreach (var res in result)
            {
                res.ImageTitleId = _blog.GetTitleImage(res.Id);
                res.AuthorName = _blog.GetAuthor(res.Id).Key;
                res.CateName = _blog.GetCategoryName(res.Id);
                res.CategoryUrl = cateName;
                res.AuthorUrl = _blog.GetAuthor(res.Id).Value;
            }
            return result;
        }
        public List<BlogDTO> GetListPopularBlogs(int top, int skip, bool des, Sorting date)

        {
            var _res = _blog.GetListPopularBlogs(top, skip, des, date);

            var res = ObjectMapper.Map<List<Blog>, List<BlogDTO>>(_res);
            foreach (var item in res)
            {
                if (item != null)
                {
                    item.ImageTitleId = _blog.GetTitleImage(item.Id);
                    item.AuthorName = _blog.GetAuthor(item.Id).Key;
                    item.CateName = _blog.GetCategoryName(item.Id);
                    item.AuthorUrl = _blog.GetAuthor(item.Id).Value;
                }

            }
            return res;
        }
        public List<BlogDTO> GetNewestBlog(int top)
        {
            var _res = _blog.GetNewestBlog(top);
            var res = ObjectMapper.Map<List<Blog>, List<BlogDTO>>(_res);
            foreach (var item in res)
            {
                item.ImageTitleId = _blog.GetTitleImage(item.Id);
                item.AuthorName = _blog.GetAuthor(item.Id).Key;
                item.CateName = _blog.GetCategoryName(item.Id);
                item.AuthorUrl = _blog.GetAuthor(item.Id).Value;
            }
            return res;
        }

        public List<BlogDTO> GetTopLikeBlogs(int top)
        {
            var _res = _blog.GetTopLikeBlogs(top);
            var res = ObjectMapper.Map<List<Blog>, List<BlogDTO>>(_res);
            foreach (var item in res)
            {
                item.ImageTitleId = _blog.GetTitleImage(item.Id);
                item.AuthorName = _blog.GetAuthor(item.Id).Key;
                item.CateName = _blog.GetCategoryName(item.Id);
                item.AuthorUrl = _blog.GetAuthor(item.Id).Value;
            }
            return res;
        }
        public List<BlogDTO> GetAllByStateService(BlogState state)
        {
            var _res = _blog.GetAllRepository(state);
            var res = ObjectMapper.Map<List<Blog>, List<BlogDTO>>(_res);
            foreach (var item in res)
            {
                item.ImageTitleId = _blog.GetTitleImage(item.Id);
                item.AuthorName = _blog.GetAuthor(item.Id).Key;
                item.CateName = _blog.GetCategoryName(item.Id);
                item.AuthorUrl = _blog.GetAuthor(item.Id).Value;
            }
            return res;
        }
        public override Task<PagedResultDto<BlogDTO>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var _res = base.GetListAsync(input);
            foreach (var item in _res.Result.Items)
            {
                //var time = DateTime.Now - item.CreationTime;
                item.ImageTitleId = _blog.GetTitleImage(item.Id);
                item.TagName = _tag.GetTagNamesByBlog(item.Id);
                item.AuthorName = _blog.GetAuthor(item.Id).Key;
                item.CateName = _blog.GetCategoryName(item.Id);
                item.AuthorUrl = _blog.GetAuthor(item.Id).Value;
            }
            return _res;
        }
        public override async Task<BlogDTO> GetAsync(Guid id)
        {
            //_blog.CountSeen(id);
            var _res = await base.GetAsync(id);
            _res.Comments = _comment.GetCommentsFromBlog(id);
            _res.ImageTitleId = _blog.GetTitleImage(id);
            _res.TagName = _tag.GetTagNamesByBlog(id);
            _res.AuthorName = _blog.GetAuthor(id).Key;
            _res.CateName = _blog.GetCategoryName(id);
            _res.AuthorUrl = _blog.GetAuthor(_res.Id).Value;
            return _res;
        }
        public async Task<BlogDTO> GetDetail(string title)
        {
            var blog = await _blog.GetDetailByTitle(title);
            var _res = ObjectMapper.Map<Blog, BlogDTO>(blog);
            _res.Comments = _comment.GetCommentsFromBlog(blog.Id);
            _res.ImageTitleId = _blog.GetTitleImage(blog.Id);
            _res.TagName = _tag.GetTagNamesByBlog(blog.Id);
            _res.AuthorName = _blog.GetAuthor(blog.Id).Key;
            _res.CateName = _blog.GetCategoryName(blog.Id);
            _res.CategoryUrl = _blog.UrlFriendly(_res.CateName);
            _res.AuthorUrl = _blog.GetAuthor(_res.Id).Value;
            return _res;
        }
        public List<BlogDTO> GetBlogsByUser(string username, BlogState state)
        {
            var Blogs = _blog.GetBlogByUser(username,state);
            var res =  ObjectMapper.Map<List<Blog>, List<BlogDTO>>(Blogs);
            foreach (var item in res)
            {
                item.ImageTitleId = _blog.GetTitleImage(item.Id);
                item.AuthorName = _blog.GetAuthor(item.Id).Key;
                item.CateName = _blog.GetCategoryName(item.Id);
                item.AuthorUrl = _blog.GetAuthor(item.Id).Value;
            }
            return res;
        }
        public List<BlogDTO> GetNewBlogByTimeService(Sorting time)
        {
            var blogs = _blog.GetNewBlogByTime(time);
            var res = ObjectMapper.Map<List<Blog>, List<BlogDTO>>(blogs);
            foreach (var item in res)
            {
                item.ImageTitleId = _blog.GetTitleImage(item.Id);
                item.AuthorName = _blog.GetAuthor(item.Id).Key;
                item.CateName = _blog.GetCategoryName(item.Id);
                item.AuthorUrl = _blog.GetAuthor(item.Id).Value;
            }
            return res;
        }
        [Authorize]
        public async Task CountSeenService(Guid id)
        {
            await _blog.CountSeen(id);

        }
        public List<BlogDTO> GetBlogsByTagName(string TagName)
        {
            var _blogs = _tag.GetBlogsByTagName(TagName);
            var res = ObjectMapper.Map<List<Blog>, List<BlogDTO>>(_blogs);
            foreach (var item in res)
            {
                item.ImageTitleId = _blog.GetTitleImage(item.Id);
                item.AuthorName = _blog.GetAuthor(item.Id).Key;
                item.CateName = _blog.GetCategoryName(item.Id);
                item.AuthorUrl = _blog.GetAuthor(item.Id).Value;
            }
            return res;
        }
        [Authorize]
        public override Task DeleteAsync(Guid id)
        {
            _blog.DeleteImageFromBlog(id);
            _tag.RemoveTagsFromBlog(id);
            _review.DeleteReviewsFromBlog(id);

            return base.DeleteAsync(id);
        }
        [Authorize(Policy = "admin")]
        public async Task ChangeBlogState(Guid id, BlogState state)
        {
            if (state == BlogState.Accepted)
            {
                await _blog.AddTracking(id);
            }
            await _blog.ChangeBlogState(id, state);
        }
        public bool ChangeCategory(Guid id, Guid newCate)
        {
            if (_blog.ChangeCategory(id, newCate)) return true;
            else throw new Exception("Category is not exist");
        }
        [Authorize]
        public async Task<BLogReviewDTO> UpdateRating(Guid idBlog, decimal point)
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            var res = await _review.UpdateRating(idBlog, Guid.Parse(currentUser), point);
            return ObjectMapper.Map<BlogReview, BLogReviewDTO>(res);
        }
        [Authorize]
        // if return false : wrong user on this blog
        public bool SendRequest(Guid idBlog)
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            return _blog.SendRequest(Guid.Parse(currentUser), idBlog);
        }
        [Authorize]
        public override Task<BlogDTO> UpdateAsync(Guid id, CreateUpdateBlogDTO input)
        {
            var currentUser = CurrentUser.FindClaim("sub").Value;
            if (_blog.GetAuthor(id).Key == _blog.GetAuthor(Guid.Parse(currentUser)).Key && _blog.GetState(id) != BlogState.Accepted)
            {
                return base.UpdateAsync(id, input);
            }
            else throw new Exception("Cannot update this blog !");
        }
        public async Task AddTracking(Guid idBlog)
        {
            await _blog.AddTracking(idBlog);
        }
        [Authorize]
        public async Task<bool> LikePostAsync(Guid idBlog)
        {
            return await _blog.LikePost(idBlog);
        }
        
    }
}

