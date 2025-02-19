using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Employee
{
    public class EmployeeController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var res = await serviceManager.EmployeeService.GetAllEmp();
            return Ok(res);
        }

        [HttpPost("GetById")]
        public async Task<ActionResult<EmployeeDto> >GetEmployeeById(int id )
        {
            var res = await serviceManager.EmployeeService.GetEmployeeById(id);
            return Ok(res);
        }


        [HttpPost("AddEmployee")]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(EmployeeDto employeeDto) 
        {
            var res = await serviceManager.EmployeeService.AddEmployee(employeeDto);
            return Ok(res);
        }


        [HttpGet("Delete_Employee")]
        public async Task<ActionResult<EmployeeDto>> DeleteEmployeee(int id)
        {
            var res = await serviceManager.EmployeeService.DeleteEmployee(id);
            return Ok(res);
        }
    }
}
