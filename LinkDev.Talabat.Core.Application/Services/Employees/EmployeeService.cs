using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
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
        //public async Task<IEnumerable<EmployeeDto>> GetAllEmp()
        public async Task<ResponseDto> GetAllEmp()
        {
            try
            {
                var spec = new EmployeeWithDepartmentSpecifications() as ISpecifications<Employee, int>;
                var emp = mapper.Map<IEnumerable<EmployeeDto>>(await unitOfWork.GetRepository<Employee, int>().GetAllWithSpecAsync(spec));

                return new ResponseDto { IsSuccess = true, Result = emp, Message = "Data retrived " };

            }
            catch (System.Exception ex )
            {

                return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

            }
        }
        public  async Task<ResponseDto> GetEmployeeById(int id)
        {
            try
            {
                var spec = new EmployeeWithDepartmentSpecifications(id) as ISpecifications<Employee, int>;
                var emp = mapper.Map<EmployeeDto>(await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec));
                return new ResponseDto { IsSuccess = true, Result = emp, Message = "Data retrived " };

            }
            catch (System.Exception ex )
            {

                return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

            }
        }
        public async Task<ResponseDto> AddEmployee(EmployeeDto employeeDto)

        {
            try
            {
                var entity = mapper.Map<Employee>(employeeDto);
                await unitOfWork.GetRepository<Employee, int>().AddAsync(entity);


                var add = await unitOfWork.CompleteAsync();
                if (add > 0)
                return new ResponseDto { IsSuccess = true, Result = add, Message = "Success" };
                    return new ResponseDto { IsSuccess = false, Result = null, Message = "Not Added " };

            }
            catch (System.Exception ex )
            {

                return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

            }

        }
        public async Task<ResponseDto> UpdateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var oldemp = await unitOfWork.GetRepository<Employee, int>().GetAsync(employeeDto.Id);
                //var entity = mapper.Map<Employee>(employeeDto);
                //unitOfWork.GetRepository<Employee, int>().Update(entity);
                if (oldemp == null)
                    return new ResponseDto { IsSuccess = true, Result = employeeDto, Message = $"Not fount with Id = {employeeDto.Id}" };

                oldemp.Name = employeeDto.Name?? oldemp.Name;
                oldemp.DepartmentId = employeeDto.DepartmentId?? oldemp.DepartmentId;
                oldemp.Age = employeeDto.Age?? oldemp.Age;
                //oldemp.Name = employeeDto.Name?? oldemp.Name;
                unitOfWork.GetRepository<Employee, int>().Update(oldemp);

                var add = await unitOfWork.CompleteAsync();
                if (add > 0)
                    return new ResponseDto { IsSuccess = true, Result = employeeDto, Message = "Success" };
                return new ResponseDto { IsSuccess = false, Result = null, Message = "Not Added " };


            }
            catch (System.Exception ex )
            {

                return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

            }

        }

        public async  Task<ResponseDto> DeleteEmployee(int id)
        {
            try
            {
                var spec = new EmployeeWithDepartmentSpecifications(id) as ISpecifications<Employee, int>;
                var obj = await unitOfWork.GetRepository<Employee, int>().GetWithSpecAsync(spec);
                var employeeDto = mapper.Map<EmployeeDto>(obj);
                unitOfWork.GetRepository<Employee, int>().Delete(obj);
                var deleted = await unitOfWork.CompleteAsync();
                if (deleted > 0)
                    return new ResponseDto { IsSuccess = true, Result = deleted, Message = "Success" };
                return new ResponseDto { IsSuccess = false, Result = null, Message = "Not Added " };

            }
            catch (System.Exception ex )
            {

                return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

            }

        }


    }
}
