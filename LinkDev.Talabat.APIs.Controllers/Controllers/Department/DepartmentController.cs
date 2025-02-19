using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Department;
using Microsoft.AspNetCore.Mvc;

namespace FastDev.Talabat.Controllers.Controllers.Department
{
    public class DepartmentController(IServiceManager serviceManager): BaseApiController
    {



        [HttpPost("Add_Department")]
        public async Task<ActionResult> AddDepartment(DepartmentCreateDto model)
        {
            var res = await serviceManager.DepartmentService.AddDept(model);
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res);
        }


        [HttpPost("Get_DepartmentById")]
        public async Task<ActionResult> GetDepartmentById(int id)
        {
            var res = await serviceManager.DepartmentService.GetDeptById(id);
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res);
        }

        [HttpGet("Get_AllDepartmenrs")]
        public async Task<ActionResult> GetAllDepartmenrs()
        {
            var res = await serviceManager.DepartmentService.GetAllDept();
            if(res.IsSuccess)
                  return Ok(res);
            return BadRequest(res);
        }
        // checl Update , delete    
        [HttpPost("Update_Department")]
        public async Task<ActionResult> UpdateDepartment(DepartmentDto model)
        {
            var res = await serviceManager.DepartmentService.UpdateDept(model);
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res);
        }

        [HttpPost("Delete_Department")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            var res = await serviceManager.DepartmentService.DeleteDept(id);
            if (res.IsSuccess)
                return Ok(res);
            return BadRequest(res);
        }
    }

}

