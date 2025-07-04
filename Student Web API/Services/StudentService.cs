using Microsoft.EntityFrameworkCore;
using Student_Web_API.Data;
using Student_Web_API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Web_API.Services
{
    public class StudentService : IStudent
    {
        private readonly StudentDbContext _context;
        public StudentService(StudentDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Student>> GetAll()
        {
            return await _context.Students.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<bool> Create(Student student)
        {
            var studentCount = _context.Students.ToList().Count;
            studentCount++;
            student.Id = studentCount;
           await  _context.Students.AddAsync(student);
            return await SaveAsync();
        }
        public async Task<bool> Exists(int id)
        {
            return await _context.Students.AnyAsync(x => x.Id == id);
        }
        public async Task<bool> Delete(int id)
        {
            var student = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            _context.Students.Remove(student);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
