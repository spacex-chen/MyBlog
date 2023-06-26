using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Common.Utility.ApiResult;
using SqlSugar;

namespace MyBlog.WebApi.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _iBlogNewsService;
        public BlogNewsController(IBlogNewsService iBlogNewsService) 
        {
            this._iBlogNewsService = iBlogNewsService;
        }

        // [AllowAnonymous]
        [HttpGet(template: "BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            int id = Convert.ToInt32(value: User.FindFirst(type: "Id")?.Value);
            var data = await _iBlogNewsService.QueryAsync(c => c.WriterId == id);

            if (data == null)
            {
                return ApiResultHelper.Error("没有更多的文章");
            }

            return ApiResultHelper.Success(data);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> Create(string title, string content, int typeid)
        {
            BlogNews blogNews = new()
            {
                BrowseCount = 0,
                Content = content,
                LikeCount = 0,
                Time = DateTime.Now,
                Title = title,
                TypeId = typeid,
                WriterId = Convert.ToInt32(value: User.FindFirst(type: "Id")?.Value)
        };

            bool b = await _iBlogNewsService.CreateAsync(blogNews);

            if (!b)
            {
                return ApiResultHelper.Error("添加失败，服务器发生错误");
            }
            return ApiResultHelper.Success(blogNews);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(dynamic[] id)
        {
            bool b = await _iBlogNewsService.DeleteAsync(id);
            if (!b)
            {
                return ApiResultHelper.Error("删除失败");
            }
            return ApiResultHelper.Success(b);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ApiResult>> Update(int id, string title, string content, int typeid)
        {
            var blogNews = await _iBlogNewsService.GetByIdAsync(id);

            if (blogNews == null )
            {
                return ApiResultHelper.Error("没有找到可修改的文章");
            }
            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeid;
            bool b = await _iBlogNewsService.UpdateAsync(blogNews);
            if (!b)
            {
                return ApiResultHelper.Error("修改失败");
            }
            return ApiResultHelper.Success(blogNews);
        }

        [HttpGet("BlogNewsPages")]
        public async Task<ApiResult> GetBlogNewsPages([FromServices] IMapper iMapper, int page, int size)
        {
            RefAsync<int> total = 0;
            var blogNews = await _iBlogNewsService.QueryAsync(page, size, total);

            try
            {
                var blogNewsDTO = iMapper.Map<List<BlogNewsDTO>>(blogNews);
                return ApiResultHelper.Success(blogNewsDTO, total);
            }
            catch (Exception)
            {
                return ApiResultHelper.Error("AutoMapper映射错误");
            }
        }
    }
}
