using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Department;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Employee;
namespace LinkDev.Talabat.Core.Application.Services.Departments
{
    public class DepartmentService(IUnitOfWork unitOfWork , IMapper mapper) : IDepartmentService
    {
        //Specs ?
        //Mapping ??
        // Validation is Important But i not have buisness here 

        public async  Task<ResponseDto> GetAllDept()
        {
            try
            
            {
                //var obj = await unitOfWork.GetRepository<Department, int>().GetAllAsync();
                var repo =  unitOfWork.GetRepository<Department, int>();
                var obj = await  repo.GetAllAsync();

                var dto = mapper.Map<IEnumerable<DepartmentDto>>(obj);

                return new ResponseDto { IsSuccess =true , Result = dto , Message = "Success"};
            }
            catch (System.Exception ex )
            {

                return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

            }

        }
        public async Task<ResponseDto> GetDeptById(int id)
        {
            /// //try
            /// //{
            /// //    var obj = await unitOfWork.GetRepository<Department, int>().GetByAsync(id);
            /// //    var dto = mapper.Map<DepartmentDto>(obj);
            /// 
            /// //    return new ResponseDto { IsSuccess = true, Result = dto, Message = "Success" };
            /// 
            /// //}
            /// //catch (Exception ex )
            /// //{
            /// 
            /// //    return new ResponseDto { IsSuccess =false , Result = null , Message = ex.Message};
            /// 
            /// //}
            /// 

            try
            {

                //var obj = await unitOfWork.GetRepository<Department, int>().GetAsync(id);
                var objIn =  unitOfWork.GetRepository<Department, int>();
                var obj = await objIn.GetAsync(id);
                var dto = mapper.Map<DepartmentDto>(obj);

                return new ResponseDto { IsSuccess = true, Result = dto, Message = "Success" };
            }
            catch (System.Exception ex )
            {

                return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

            }

        }
        public async Task<ResponseDto> AddDept(DepartmentCreateDto model)
        {
            // To be checked 
            // if dep name already exist ?

            //try
            //{
            //    var obj = mapper.Map<Department>(model);  
            //    var add = await unitOfWork.GetRepository<Department, int >() .AddAsync(obj);
            //    await unitOfWork.CompeleteAsync();


            //    if (add == null )
            //        return new ResponseDto { IsSuccess = false, Result = null, Message = "Not Added " };
            //    return new ResponseDto { IsSuccess = true, Result = add, Message = "Success" };


            //}
            //catch (Exception ex  )
            //{

            //    return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

            //}


            try
            {
                var obj = mapper.Map<Department>(model);
                await unitOfWork.GetRepository<Department, int>().AddAsync(obj);
                //await unitOfWork.CompleteAsync();

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
            public async  Task<ResponseDto> UpdateDept(DepartmentDto model)
             {
                // To do : Check if dep name already exist ?
                //try
                //{
                //    var obj = mapper.Map<Department>(model);
                //    unitOfWork.GetRepository<Department, int>().Update(obj);

                //   //var isUpdated =  unitOfWork.CompeleteAsync();

                //    if (unitOfWork.CompeleteAsync() is not null)
                //        return new ResponseDto { IsSuccess = false, Result = null, Message = "Not UPDATED " };
                //    return new ResponseDto { IsSuccess = true, Result = model, Message = "Success" };


                //}
                //catch (Exception ex)
                //{

                //    return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

                //}
                var obj = mapper.Map<Department>(model);
                unitOfWork.GetRepository<Department, int>().Update(obj);

                //var isUpdated =  unitOfWork.CompeleteAsync();

                if (unitOfWork.CompleteAsync() is not null)
                    return new ResponseDto { IsSuccess = false, Result = null, Message = "Not UPDATED " };
                return new ResponseDto { IsSuccess = true, Result = model, Message = "Success" };


                   }
            public async Task<ResponseDto> DeleteDept(int id)
              {
            //try
            //{
            //    var obj = await unitOfWork .GetRepository<Department,int >().GetByAsync(id);
            //    // To Do : If not exist return response 
            //      unitOfWork.GetRepository<Department,int>().Delete(obj);
            //    var isUpdated = unitOfWork.CompeleteAsync();

            //    if (isUpdated.IsCompletedSuccessfully)
            //        return new ResponseDto { IsSuccess = false, Result = null, Message = "Not Added " };
            //    return new ResponseDto { IsSuccess = true, Result = $"Deleted Id = {id}", Message = "Success" };


            //}
            //catch (Exception ex )
            //{

            //    return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };

            //}
            try
            {
                var obj = await unitOfWork.GetRepository<Department, int>().GetAsync(id);
                // To Do : If not exist return response 
                unitOfWork.GetRepository<Department, int>().Delete(obj);
                var isUpdated = unitOfWork.CompleteAsync();

                if (isUpdated.IsCompletedSuccessfully)
                    return new ResponseDto { IsSuccess = false, Result = null, Message = "Not Added " };
                return new ResponseDto { IsSuccess = true, Result = $"Deleted Id = {id}", Message = "Success" };
            }
            catch (System.Exception ex)
            {

                return new ResponseDto { IsSuccess = false, Result = null, Message = ex.Message };
            }

        }


    }
}
