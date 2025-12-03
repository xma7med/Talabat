using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Employee
{
    public class EmployeeController(IServiceManager serviceManager) : BaseApiController
    {



        [HttpPost("AddEmployee")]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(EmployeeDto employeeDto)
        {
            var res = await serviceManager.EmployeeService.AddEmployee(employeeDto);
            return Ok(res);
        }


        [HttpPost("GetById")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            var res = await serviceManager.EmployeeService.GetEmployeeById(id);
            return Ok(res);
        } 

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var res = await serviceManager.EmployeeService.GetAllEmp();
            return Ok(res);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<EmployeeDto>> Update(EmployeeDto employee)
        {
            var res = await serviceManager.EmployeeService.UpdateEmployee(employee);
            return Ok(res);
        }

<<<<<<< HEAD

        [HttpPost("AddEmployee")]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(EmployeeDto employeeDto) 
        {
            var res = await serviceManager.EmployeeService.AddEmployee(employeeDto);
            return Ok(res);
        }

        [HttpPost("UpdateEmployee")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee(EmployeeDto employeeDto)
        {
            var res = await serviceManager.EmployeeService.UpdateEmployee(employeeDto);
            return Ok(res);
        }


=======
>>>>>>> a293d03e47a7174e40cf74780d571906eeef1c87
        [HttpGet("Delete_Employee")]
        public async Task<ActionResult<EmployeeDto>> DeleteEmployeee(int id)
        {
            var res = await serviceManager.EmployeeService.DeleteEmployee(id);
            return Ok(res);
        }
    }
}
