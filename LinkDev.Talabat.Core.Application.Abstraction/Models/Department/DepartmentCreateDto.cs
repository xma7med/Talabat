namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Department
{
    public class DepartmentCreateDto
    {
        public int Id { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public required string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;
        public required string Name { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
