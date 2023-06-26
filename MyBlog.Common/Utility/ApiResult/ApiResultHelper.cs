using SqlSugar;

namespace MyBlog.Common.Utility.ApiResult
{
    public static class ApiResultHelper
    {
        public static ApiResult Success(dynamic data)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Message = "操作成功",
                Total = 0
            };
        }
        public static ApiResult Success(dynamic data, RefAsync<int> total)
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Message = "操作成功",
                Total = total
            };
        }

        public static ApiResult Error(string message) 
        {
            return new ApiResult
            {
                Code = 500,
                Data = message,
                Message = message,
                Total = 0
            };
        }
    }
}
