using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using QLTH.Data;
using QLTH.Models.DTO;

namespace QLTH.Controller
{
    [Route("api/School")]
    [ApiController]
    public class SchoolManagement : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SchoolManagement(ApplicationDbContext db)
        {
            _db = db;
        }

        public readonly ILogger<SchoolManagement> _logger;

        public SchoolManagement(ILogger<SchoolManagement> logger)
        {
            _logger = logger;
        }



        // todo Ham lay danh sach truong hoc
        [HttpGet]
        public ActionResult<IEnumerable<SchoolDTO>> GetSchools()
        {
            _logger.LogInformation("Get All school");
            // return Ok(SchoolStore.schoolList);
            return Ok(_db.Schools.ToList());
        }

        //todo Ham lay thong tin 1 vila
        [HttpGet("{id:int}", Name = "GetOne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SchoolDTO> GetSchools(int id)
        {
            if (id == 0)
            {
                _logger.LogError("GetOne Error with Id" + id);
                return BadRequest();
            }
            var school = _db.Schools.FirstOrDefault(item => item.Id == id);
            if (school == null)
            {
                return NotFound();
            }
            return Ok(school);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SchoolDTO> CreateSchool([FromBody] SchoolDTO schoolDTO)
        {
            if (_db.Schools.FirstOrDefault(item => item.Name.ToLower() == schoolDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "SchoolName already Exists!");
                return BadRequest(ModelState);
            }
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (schoolDTO == null)
            {
                return BadRequest();
            }
            if (schoolDTO.Id < 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var existSchool = _db.Schools.FirstOrDefault(item => item.Id == schoolDTO.Id);

            if (existSchool != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            return CreatedAtRoute("GetOne", new { id = schoolDTO.Id }, schoolDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteSchool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteSchool(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var school = _db.Schools.FirstOrDefault(item => item.Id == id);
            if (school == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateSchool(int id, [FromBody] SchoolDTO schoolDTO)
        {
            if (schoolDTO == null || id != schoolDTO.Id) { return BadRequest(); }
            var school = SchoolStore.schoolList.FirstOrDefault(item => item.Id == id);

            if (school == null)
            {
                return BadRequest();
            }

            school.Name = schoolDTO.Name;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialSchool")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialSchool(int id, JsonPatchDocument<SchoolDTO> patchDTO)
        {
            if (patchDTO == null || id == 0) { return BadRequest(); }
            var school = SchoolStore.schoolList.FirstOrDefault(item => item.Id == id);

            if (school == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(school, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent() ;
        }
    }
};
