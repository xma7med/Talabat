namespace LinkDev.Talabat.Core.Domain.Entities.Employee
{
    public class Department:BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        public DateTime CreationDate { get; set; }

    }
}
