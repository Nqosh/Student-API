using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student_Web_API.Services
{
    public interface IStudent
    {
        Task<ICollection<Model.Student>> GetAll();

        Task<bool> Create(Model.Student student);
        Task<bool> Exists(int id);


        Task<bool> Delete(int id);
    }
}
