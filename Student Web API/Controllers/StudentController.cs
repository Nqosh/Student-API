using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Web_API.DTOs;
using Student_Web_API.Model;
using Student_Web_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Student_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudent _studentService;

        public StudentController(IMapper mapper, IStudent _studentService)
        {
            this._mapper = mapper;
            this._studentService = _studentService;
        }

        // GET: api/<StudentController>
        [HttpGet("List")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetAll();
            var studentsDto = new List<StudentDto>();

            foreach (var student in students)
                studentsDto.Add(_mapper.Map<StudentDto>(student));
            return Ok(studentsDto);
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentController>
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] StudentDto studentDto)
        {
            if (studentDto == null)
                return BadRequest(ModelState);

            var student = _mapper.Map<Student>(studentDto);

            if (!await _studentService.Create(student))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record { student.Name }");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _studentService.Exists(id))
                return NotFound();

            if (!await this._studentService.Delete(id))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record { id }");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }
    }
}
