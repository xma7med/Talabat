namespace LinkDev.Talabat.Core.Domain.Entities.Employee
{
    /// 1 employee work in one department  
    // 1 department have many employee
    // Employee 1 - 1 Dept
    // Employee M-1 Dept 
    // => Employee M - 1 Depatment 
    /// FK from 1 to many 
    public class Employee:BaseAuditableEntity<int>
    {
        public required string Name   { get; set; }
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public int? DepartmentId { get; set; }
        // This is a navigation property to the Department entity

        public virtual Department? Department { get; set; }
    }
}
