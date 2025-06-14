namespace LinkDev.Talabat.Core.Domain.Entities.Employee
{
    public class Department:BaseAuditableEntity<int>
    {
        //Id 
        public required string Name { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;  

        //public string CreatedBy { get; set; } = null!; // 3 way will use inspector 

        //public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        //public string LastModifiedBy { get; set; } = null!;
        //public DateTime LastModifiedOn { get; set; } = DateTime.UtcNow;

    }
}
