using LinkDev.Talabat.Core.Application.Abstraction.Models.Employee;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Employees
{
	public interface IEmployeeService
	{
		Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync();
		Task<EmployeeToReturnDto> GetEmployeeAsync(int id );
	}
}
