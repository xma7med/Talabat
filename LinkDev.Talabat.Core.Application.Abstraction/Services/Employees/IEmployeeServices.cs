using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Employees
{
    public interface IEmployeeServices
    {
        Task<ResponseDto> GetAllEmp();
        //Task<IEnumerable<EmployeeDto>> GetAllEmp();
        Task<ResponseDto> GetEmployeeById(int id);

        Task<ResponseDto> AddEmployee(EmployeeDto employeeDto);
        Task<ResponseDto> UpdateEmployee(EmployeeDto employeeDto);  
        Task<ResponseDto> DeleteEmployee(int id);   
    }
}
