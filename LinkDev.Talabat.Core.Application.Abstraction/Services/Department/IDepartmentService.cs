
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Department;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services
{
    // Add get Emp in Dept 
    public interface IDepartmentService
    {
        Task<ResponseDto> GetAllDept();
        Task<ResponseDto> GetDeptById(int id);
        Task<ResponseDto> AddDept(DepartmentCreateDto model);
        Task<ResponseDto> UpdateDept(DepartmentDto model);
        Task<ResponseDto> DeleteDept(int id);
    }
}
