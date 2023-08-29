using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QLTH.Models.DTO;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Cryptography;
using Azure.Core;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using QLTH.Models.RequestDTO;

namespace QLTH.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly IConfiguration _configuration;
        //private readonly JwtConfig _jwtConfig;
        public Account(IConfiguration configuration)
        {
            _configuration = configuration;
           // _jwtConfig = jwtConfig;

        }

        // api Tạo tài khoản
        public static User user = new User();

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<Registrtion>> registration(UserDTO request) {
            CreatePasswordHash(request.Password, out byte[] passwordhash, out byte[] passwordSalt);
            user.Username = request.Username;
            user.PasswordHash = passwordhash;
            user.PasswordSalt = passwordSalt;
            var connectString = _configuration["ConnectionStrings:DefaultSQLConnection"].ToString() + ";trustServerCertificate=true";
           // SqlConnection con = new SqlConnection(connectString);
            // SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName,Password,email,IsActive)VALUES('" + DTO.UserName+"',' "+ DTO.Password + "','" + DTO.Email + "','" + DTO.isActive + "')", con);
            //con.Open();
            //int i = cmd.ExecuteNonQuery();
            //con.Close();
            return Ok(user);
        }

        // api đăng nhập -> bổ sung jwt, access token cho mỗi lần đăng nhập
        // tạo hàm mã hóa, giải mã mật khẩu -> check điều kiện đăng nhập
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(LoginRequest request)
        {
            var connectString = _configuration["ConnectionStrings:DefaultSQLConnection"].ToString() + ";trustServerCertificate=true";
            SqlConnection con = new SqlConnection(connectString);
            SqlDataAdapter da = new SqlDataAdapter("Select * from Account where UserName = '" + request.Username + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                DataRow userExist = dt.Rows[0];
                // Lấy ra mật khẩu
                string userName = userExist["Username"].ToString();
                string password = userExist["password"].ToString();
                // byte[] bytesPass = Encoding.ASCII.GetBytes(password);
                // byte[] bytesPassSalt = Encoding.ASCII.GetBytes("1234");

                //Check trùng mật khẩu
                //if (!VerifyPasswordHash(request.Password, bytesPass, bytesPassSalt))
                //{
                //    var token = GenerateJwtToken(new UserDTO(userName, password, "19020217@vnu.edu.vn", 1));
                //}
                if(request.Password == password)
                {
                    var token = GenerateJwtToken(new UserDTO(userName, password, "19020217@vnu.edu.vn", 1));
                    return Ok(new ResponseMessage(token, 0, true, false)); ;
                } else
                {
                    return BadRequest("Tên đăng nhập hoặc mật khẩu không chính xác");
                }
                
            }
            else { return BadRequest("Tên đăng nhập hoặc mật khẩu không chính xác"); }
        }

        [HttpGet]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            return Ok(new ResponseMessage("", 0, true, false));
        }


        // api lấy thông tin tài khoản
        [HttpGet]
        [Route("getOne")]
        public async Task<ActionResult> GetOne(Guid id)
        {
            return Ok("123");
        }

        // api Xóa người dùng -> đặt sang trạng thái không hoạt động
        [HttpGet]
        [Route("deleteOne")]
        public async Task<ActionResult> DeleteOne(Guid id)
        {
            return Ok("123");
        }




        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }



        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string GenerateJwtToken(UserDTO user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes("1fiFxnaBZJbZHdSJBDdN");

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
                }),

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class LoginResponse
        {
            public string Token { get; set; }
            public int Status { get; set; }
        }

    }
}
