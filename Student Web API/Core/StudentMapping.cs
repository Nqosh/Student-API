using AutoMapper;

namespace Student_Web_API.Core
{
    public class StudentMapping : Profile
    {
        public StudentMapping()
        {
            CreateMap<Model.Student, DTOs.StudentDto>().ReverseMap();
        }
    }
}
