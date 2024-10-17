namespace LinkDev.Talabat.Core.Domain.Entities.Employees
{
	public class Employee:BaseAuditableEntity<int>
	{
		public required string Name { get; set; }
		public int? Age { get; set; }
		public decimal Salary { get; set; }

        public int? DepartmentId { get; set; }

        // This is a navigation property to the Department entity
        public virtual Department? Department { get; set; }
	}
}
