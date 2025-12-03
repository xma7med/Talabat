namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Department
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public  string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public required string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
