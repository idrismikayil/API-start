using AutoMapper;
using Data.DAL;
using Data.Mapper;
using Domain.Dtos;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.ToListAsync();

            var studentDto = _mapper.Map<List<StudentDto>>(students);

            return Ok(studentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var students = await _context.Students.FindAsync(id);

            var studentDto = _mapper.Map<StudentDto>(students);

            return Ok(studentDto);
        }
    }
}
