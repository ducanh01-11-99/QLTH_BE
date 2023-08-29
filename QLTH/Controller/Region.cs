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
using System.Collections;
using QLTH.Models.RegionDTO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace QLTH.Controller
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]

    public class RegionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // api get all province
        [HttpGet]
        [Route("getAllProvinceVN")]
        public async Task<ActionResult<Registrtion>> getAllProvince()
        {
            var connectString = _configuration["ConnectionStrings:DefaultSQLConnection"].ToString() + ";trustServerCertificate=true";
            SqlConnection con = new SqlConnection(connectString);
            SqlDataAdapter da = new SqlDataAdapter("Select * from  provinces", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<ProvinceDTO> list = new List<ProvinceDTO>();

            foreach (DataRow dataRow in dt.Rows)
            {
                var tempData = new ProvinceDTO((dataRow["code"].ToString()), dataRow["name"].ToString(), dataRow["name_en"].ToString(), dataRow["full_name"].ToString(), dataRow["full_name_en"].ToString(), dataRow["code_name"].ToString(), Convert.ToInt32(dataRow["administrative_unit_id"]), Convert.ToInt32(dataRow["administrative_unit_id"]));
                list.Add(tempData);
            }
            

            if (list.Count > 0)
            {
               return Ok(new ResponseMessage(list, 0, true, false));

            }
            else { return BadRequest("Notfound"); }
        }
        // api get all district in province
        [HttpGet]
        [Route("getDistrictsByCode")]
        public async Task<ActionResult> getDistrictByCode(string provinceCode)
        {
            var connectString = _configuration["ConnectionStrings:DefaultSQLConnection"].ToString() + ";trustServerCertificate=true";
            SqlConnection con = new SqlConnection(connectString);
            SqlDataAdapter da = new SqlDataAdapter("Select * from  districts where province_code = '" + provinceCode + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<DistrictDTO> districts = new List<DistrictDTO>();
            foreach (DataRow dataRow in dt.Rows)
            {
                var tempData = new DistrictDTO((dataRow["code"].ToString()), dataRow["name"].ToString(), dataRow["name_en"].ToString(), dataRow["full_name"].ToString(), dataRow["full_name_en"].ToString(), dataRow["code_name"].ToString(), dataRow["province_code"].ToString(), Convert.ToInt32(dataRow["administrative_unit_id"]));
                districts.Add(tempData);
            }


            if (districts.Count > 0)
            {
                return Ok(new ResponseMessage(districts, 0, true, false));

            }
            else { return BadRequest("Notfound"); }
        }

        // api get all ward in disstrict
        [HttpGet]
        [Route("getWardsByCode")]
        public async Task<ActionResult> getWardsByCode(string districtCode)
        {
            var connectString = _configuration["ConnectionStrings:DefaultSQLConnection"].ToString() + ";trustServerCertificate=true";
            SqlConnection con = new SqlConnection(connectString);
            SqlDataAdapter da = new SqlDataAdapter("Select * from  wards where district_code = '" + districtCode + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<WardDTO> wards = new List<WardDTO>();
            foreach (DataRow dataRow in dt.Rows)
            {
                var tempData = new WardDTO((dataRow["code"].ToString()), dataRow["name"].ToString(), dataRow["name_en"].ToString(), dataRow["full_name"].ToString(), dataRow["full_name_en"].ToString(), dataRow["code_name"].ToString(), dataRow["district_code"].ToString(), Convert.ToInt32(dataRow["administrative_unit_id"]));
                wards.Add(tempData);
            }


            if (wards.Count > 0)
            {
                return Ok(new ResponseMessage(wards, 0, true, false));

            }
            else { return BadRequest("Notfound"); }
        }
    }
}
