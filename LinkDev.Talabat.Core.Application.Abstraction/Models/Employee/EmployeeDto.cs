namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; } //**
        public required string Name { get; set; }
        public int? Age { get; set; }
        public decimal? Salary { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }//** 
    }

    //public class EmployeeDto
    //{
    //    public int Id { get; set; } //**
    //    public required string Name { get; set; }
    //    public int? Age { get; set; }
    //    public decimal? Salary { get; set; }
    //    public int? DepartmentId { get; set; }
    //    public string? DepartmentName { get; set; }//** 
    //}
}
