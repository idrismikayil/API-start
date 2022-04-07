using API.Pagination;
using AutoMapper;
using Data.DAL;
using Data.Mapper;
using Data.Services.Abstraction;
using Domain.Dtos;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]

    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryService<Student> _service;

        public StudentsController(IRepositoryService<Student> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetStudents(string name)
        //{
        //    var students = await _service.GetAllAsync(s => s.Name.Contains(name));

        //    var studentDto = _mapper.Map<List<StudentDto>>(students);

        //    return Ok(studentDto);
        //}

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _service.GetAll();

            var studentDto = _mapper.Map<List<StudentDto>>(students);

            return Ok(studentDto);
        }

        [HttpGet("pagination")]
        public IActionResult GetStudent(int page, int size)
        {
            var items = _service.GetAllContext();

            var pagination = new PaginationDto<Student>(items, page, size);

            return Ok(pagination);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentPostDto studentPost)
        {
            var student = _mapper.Map<Student>(studentPost);
            await _service.Create(student);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StudentPostDto student)
        {
            var s = await _service.GetOneAsync(id);
            if(s == null) return NotFound();
            s.Name = student.Name;
            s.Age = student.Age;
            s.Surname = student.Surname;

            await _service.Update(s);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var s = await _service.GetOneAsync(id);
            if (s == null) return NotFound();
            await _service.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
