namespace Talabat.Dashboard.Models
{
    public class UserViweModel
    {
        public string Id { get; set; }
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
