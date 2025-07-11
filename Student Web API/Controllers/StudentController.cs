﻿using AutoMapper;
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

        public StudentController(IMapper mapper, IStudent studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetAll();
            var studentsDto = new List<StudentDto>();

            foreach (var student in students)
                studentsDto.Add(_mapper.Map<StudentDto>(student));
            return Ok(studentsDto);
        }

        [HttpPost("CreateStudent")]
        public async Task<IActionResult> Post([FromBody] StudentDto studentDto)
        {
            if (studentDto == null)
                return BadRequest(ModelState);

            var student = _mapper.Map<Student>(studentDto);

            if (!await _studentService.Create(student))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the Student { student.Name }");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }

        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _studentService.Exists(id))
                return NotFound();

            if (!await this._studentService.Delete(id))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the student { id }");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }
    }
}
