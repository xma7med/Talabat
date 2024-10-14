using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employee;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Employees;
using LinkDev.Talabat.Core.Domain.Specifications.Employees;

namespace LinkDev.Talabat.Core.Application.Services.Employees
{
	internal class EmployeeService(IUnitOfWork unitOfWork , IMapper mapper) : IEmployeeService
	{

		public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync()
		{
			// make the specification for employee first 
			var spec = new EmployeeWithDepartmentSpecifications();

			var employees = await unitOfWork.GetRepository<Employee , int >().GetAllWithSpecAsync(spec);	

			var employeesToReturn = mapper.Map<IEnumerable<EmployeeToReturnDto>>(employees);
			return employeesToReturn;
		}


		public async Task<EmployeeToReturnDto> GetEmployeeAsync(int id)
		{
			// Build Specification object 
			var spec = new EmployeeWithDepartmentSpecifications(id);
			var employee = await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);
			var employeesToReturn = mapper.Map<EmployeeToReturnDto>(employee);

			return employeesToReturn;	


		}


	}
}
