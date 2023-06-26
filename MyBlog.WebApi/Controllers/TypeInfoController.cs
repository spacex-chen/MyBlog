using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Common.Utility.ApiResult;

namespace MyBlog.WebApi.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class TypeInfoController : ControllerBase
    {
        private readonly ITypeInfoService _iTypeInfoService;
        public TypeInfoController(ITypeInfoService iTypeInfoService) 
        { 
            this._iTypeInfoService = iTypeInfoService;
        }

        [HttpGet("TypeInfo")]
        public async Task<ApiResult> GetTypeInfo() 
        {
            var typeinfo = await _iTypeInfoService.QueryAsync();
            if (typeinfo.Count == 0) { return ApiResultHelper.Error("没有更多的类型"); }
            return ApiResultHelper.Success(typeinfo);
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name)
        {
            #region 数据验证
            if (String.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("类型名不能为空");
            #endregion

            TypeInfo tpyeinfo = new()
            {
                Name = name
            };
            bool b = await _iTypeInfoService.CreateAsync(tpyeinfo);
            if (!b)
            {
                return ApiResultHelper.Error("类型添加失败");
            }
            return ApiResultHelper.Success(b);
        }

        [HttpPut("Update")]
        public async Task<ApiResult> Update(int id, string name)
        {
            var typeinfo = await _iTypeInfoService.GetByIdAsync(id);

            if (typeinfo == null) return ApiResultHelper.Error("没有该文章类型");
            typeinfo.Name = name;
            bool b = await _iTypeInfoService.UpdateAsync(typeinfo);
            if (!b)
            {
                return ApiResultHelper.Error("修改失败");
            }
            return ApiResultHelper.Success(typeinfo);
        }

        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(dynamic[] id)
        {
            bool b = await _iTypeInfoService.DeleteAsync(id);
            if (!b)
            {
                return ApiResultHelper.Error("删除失败");
            }
            return ApiResultHelper.Success(b);
        }
    }
}
