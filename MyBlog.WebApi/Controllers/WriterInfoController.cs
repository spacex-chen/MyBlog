using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Common.Utility.MD5;
using MyBlog.Common.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Cors;

namespace MyBlog.WebApi.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WriterInfoController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;
        public WriterInfoController(IWriterInfoService iWriterInfoService) {

            this._iWriterInfoService = iWriterInfoService;        
        }

        [AllowAnonymous]
        [HttpGet("WriterInfoById")]
        public async Task<ApiResult> GetWriterInfoByIdAsync([FromServices] IMapper iMapper, int id)
        {
            var writerinfo = await _iWriterInfoService.GetByIdAsync(id);
            var writerinfoDTO = iMapper.Map<WriterInfoDTO>(writerinfo);
            return ApiResultHelper.Success(writerinfoDTO);
        }

        [HttpGet("WriterInfo")]
        public async Task<ApiResult> GetWriterInfoAsync()
        {
            var listwriterinfo = await _iWriterInfoService.QueryAsync();
            
            if (listwriterinfo.Count == 0) { return ApiResultHelper.Error("没有更多的作者"); }

            /*foreach (var item in listwriterinfo)
            {
                var NewWriterInfoDTO = iMapper.Map<WriterInfo>(item);
                return ApiResultHelper.Success(NewWriterInfoDTO);
            }*/
            return ApiResultHelper.Success(listwriterinfo);
        }

        [HttpPost("Create")]
        public async Task<ApiResult> CreateAsync(string name, string username, string userpwd)
        {
            #region 数据验证
            if (String.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("作者名不能为空");
            if (String.IsNullOrWhiteSpace(username)) return ApiResultHelper.Error("用户名不能为空");
            if (String.IsNullOrWhiteSpace(userpwd)) return ApiResultHelper.Error("密码不能为空");
            #endregion

            WriterInfo writerinfo = new()
            {
                Name = name,
                UserName = username,
                // 对密码使用MD5加密
                UserPwd = MD5Helper.MD5Encrypt32(userpwd)
            };

            var oldWtInfo = await _iWriterInfoService.GetFindAsync(c=>c.UserName == username);
            if (oldWtInfo != null) { return ApiResultHelper.Error("已经存在此用户"); }
            bool b = await _iWriterInfoService.CreateAsync(writerinfo);
            if (!b)
            {
                return ApiResultHelper.Error("作者添加失败");
            }
            return ApiResultHelper.Success(writerinfo);
        }

        [HttpPut("Update")]
        public async Task<ApiResult> UpdateAsync(string name)
        {
            int id = Convert.ToInt32(value: User.FindFirst(type: "Id")?.Value);

            var writerinfo = await _iWriterInfoService.GetByIdAsync(id);

            if (writerinfo == null) return ApiResultHelper.Error("没有找到该作者");

            writerinfo.Name = name;
       
            bool b = await _iWriterInfoService.UpdateAsync(writerinfo);
            if (!b)
            {
                return ApiResultHelper.Error("修改失败");
            }
            return ApiResultHelper.Success(writerinfo);
        }

        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(dynamic[] id)
        {
            bool b = await _iWriterInfoService.DeleteAsync(id);
            if (!b)
            {
                return ApiResultHelper.Error("删除失败");
            }
            return ApiResultHelper.Success(b);
        }
    }
}
