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
        Task<IEnumerable<EmployeeDto>> GetAllEmp();
        Task<EmployeeDto> GetEmployeeById(int id);

        Task<EmployeeDto> AddEmployee(EmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto);  
        Task<EmployeeDto> DeleteEmployee(int id);   
    }
}
