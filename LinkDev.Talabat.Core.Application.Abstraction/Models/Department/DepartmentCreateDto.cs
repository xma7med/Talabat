namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Department
{
    public class DepartmentCreateDto
    {
        //public int Id { get; set; } = 0;
        public required string Name { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;





        //public string CreatedBy { get; set; } = string.Empty;
        //public DateTime CreatedOn { get; set; } = DateTime.Now;
        //public string LastModifiedBy { get; set; } = string.Empty;
        //public DateTime LastModifiedOn { get; set; } = DateTime.Now;

    }
}
