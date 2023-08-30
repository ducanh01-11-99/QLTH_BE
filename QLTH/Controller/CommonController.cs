using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QLTH.Controller
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Common[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        //private readonly JwtConfig _jwtConfig;
        public CommonController(IConfiguration configuration)
        {
            _configuration = configuration;
            // _jwtConfig = jwtConfig;

        }

        // api lấy danh sách giới tính
        //[HttpGet]
        //[Route("GioiTinh")]

        //api lấy danh sách dân tộc
        //api lấy danh sách 
    }
}
