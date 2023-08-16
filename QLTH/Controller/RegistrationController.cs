using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QLTH.Models.DTO;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace QLTH.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("reistration")]
        public string registration(Registrtion registrtion) {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=QLTH;Integrated Security=True;trustServerCertificate=true");
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName,Password,email,IsActive)VALUES('" + registrtion.UserName+"',' "+ registrtion.Password + "','" + registrtion.Email + "','" + registrtion.isActive + "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Data inserted";
            } else
            {
                return "Error";
            }
        }

        [HttpPost]
        [Route("login")]
        public string login(Registrtion registrtion)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=QLTH;Integrated Security=True;trustServerCertificate=true");
            SqlDataAdapter da = new SqlDataAdapter("Select * from Registration where Email = '" + registrtion.Email + "' AND Password = '" + registrtion.Password +"'AND IsActive = '" + registrtion.isActive + "')", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                return "Data Found";
            }
            else { return "Error"; }
        }
    }
}
