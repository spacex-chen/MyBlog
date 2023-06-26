using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.IService;
using MyBlog.Common.Utility.ApiResult;
using MyBlog.Common.Utility.MD5;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyBlog.Model;

namespace MyBlog.JWT.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;
        public AuthoizeController(IWriterInfoService iWriterInfoService)
        {
            _iWriterInfoService = iWriterInfoService;
        }

        [HttpPost("Login")]
        public async Task<ApiResult> Login(WriterInfo writeParam)
        {
            //加密后的密码
            string upwd = MD5Helper.MD5Encrypt32(writeParam.UserPwd);
            //数据校验
            var writerinfo = await _iWriterInfoService.GetFindAsync(c => c.UserName == writeParam.UserName && c.UserPwd == upwd);
            if (writerinfo != null)
            {
                //登录成功
                var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, writerinfo.Name),
                new Claim("Id", writerinfo.Id.ToString()),
                new Claim("UserName", writerinfo.UserName)
                //不能放敏感信息 
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-SADHJVF-SADFDSAF-EWFWAFDSAF-EWQFWQFDWA-FDSA3TRIUWYO"));
                //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:6060",
                    audience: "http://localhost:5050",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(jwtToken);
            }
            else
            {
                return ApiResultHelper.Error("账号或密码错误");
            }
        }
    }
}