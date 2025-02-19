using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employee;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Employee;
using LinkDev.Talabat.Core.Domain.Specifications.Employees;

namespace LinkDev.Talabat.Core.Application.Services.Employees
{
    public class EmployeeService (IUnitOfWork   unitOfWork , IMapper mapper): IEmployeeServices
    {
        public async Task<IEnumerable<EmployeeDto>> GetAllEmp()
        {
            var spec = new EmployeeWithDepartmentSpecifications() as ISpecifications<Employee,int>;
            var emp = mapper.Map<IEnumerable<EmployeeDto>>(await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec));
           
            return emp;
        }
        public  async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifications(id) as ISpecifications<Employee, int>;
            var emp = mapper.Map<EmployeeDto>(await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec));
            return emp;
        }
        public async Task<EmployeeDto> AddEmployee(EmployeeDto employeeDto)

        {
            var entity = mapper.Map<Employee>(employeeDto);
             await unitOfWork.GetRepository<Employee, int>().AddAsync(entity);
            return employeeDto;

        }
        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto)
        {
            var entity = mapper.Map<Employee>(employeeDto);
            unitOfWork.GetRepository<Employee, int>().Update(entity);
            return employeeDto;

        }

        public async  Task<EmployeeDto> DeleteEmployee(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifications(id) as ISpecifications<Employee, int>;
            var obj = await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);
            var employeeDto = mapper.Map<EmployeeDto>(obj);
             unitOfWork.GetRepository<Employee, int>().Delete(obj);
            return employeeDto;

        }



    }
}
