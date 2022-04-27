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
            return _context.Students.OrderBy(x => x.Id).ToList();
        }

        public async Task<bool> Create(Student student)
        {
            var studentCount = _context.Students.ToList().Count;
            studentCount++;
            student.Id = studentCount;
            _context.Students.Add(student);
            return await Save();
        }
        public async Task<bool> Exists(int id)
        {
            return _context.Students.Any(x => x.Id == id);
        }
        public async Task<bool> Delete(int id)
        {
            var student = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            _context.Students.Remove(student);
            return await Save();
        }
        public async Task<bool> Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
