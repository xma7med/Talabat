namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Department
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public required string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public required string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
