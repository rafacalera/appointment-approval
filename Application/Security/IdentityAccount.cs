namespace TimeEntryApproval.API.Application.Security
{
    public class IdentityAccount
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}
