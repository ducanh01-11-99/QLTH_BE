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
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using QLTH.Utilities;

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
            var encryptedPass = Encrypt(request.Password);
            // hàm validate MK
            if(!ValidatePassword.Validate(request.Password))
            {
                return BadRequest("Not OK");
            }
            // hàm validate username

            // hàm check trùng userName

            // Hàm tạo tài khoản mới
            var connectString = _configuration["ConnectionStrings:DefaultSQLConnection"].ToString() + ";trustServerCertificate=true";
            SqlConnection con = new SqlConnection(connectString);
            con.Open();
            SqlCommand command = new SqlCommand("Insert Into Account (UserName, Password) Values (@userName, @password)", con);

            // Cung cấp các giá trị cho các tham số
            command.Parameters.AddWithValue("@userName", request.Username);
            command.Parameters.AddWithValue("@password", encryptedPass);

            // Thực thi câu lệnh
            int affectedRows = command.ExecuteNonQuery();
            var reMess = "";

            // Kiểm tra xem câu lệnh có được thực hiện thành công hay không
            if (affectedRows > 0)
            {
                // Câu lệnh được thực hiện thành công
                reMess = "Câu lệnh được thực hiện thành công";
            }
            else
            {
                // Câu lệnh không được thực hiện thành công
                reMess = "Câu lệnh không được thực hiện thành công";
            }
            con.Close();


            return Ok(reMess);
        }


        // api đăng nhập -> bổ sung jwt, access token cho mỗi lần đăng nhập
        // tạo hàm mã hóa, giải mã mật khẩu -> check điều kiện đăng nhập
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserRegistrationRequestDto request)
        {
            var connectString = _configuration["ConnectionStrings:DefaultSQLConnection"].ToString() + ";trustServerCertificate=true";
            SqlConnection con = new SqlConnection(connectString);
            SqlDataAdapter da = new SqlDataAdapter("Select * from Account where UserName = '" + request.Name + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                DataRow userExist = dt.Rows[0];
                // Lấy ra mật khẩu
                string userName = userExist["Username"].ToString();
                string password = userExist["password"].ToString();

                var newUser = new IdentityUser()
                {
                    Email = "",
                    UserName = userName,
                };

                if (Decrypt(password) == request.Password)
                {
                    var token = GenerateJwtToken(newUser);
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



        // Encryp Password
        private static string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private static string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);
            //var key = Encoding.UTF8.GetBytes("HjTkqJTiMtS0pcML0C4B");
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, value: user.Email),
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
