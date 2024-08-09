namespace TimeEntryApproval.API.Application.Security
{
    public class IdentityService
    {
        public IdentityAccount GetAccount()
        {
            return new IdentityAccount()
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "John Doe",
                Email = "john@email.com",
                Role = 1
            };
        }
    }
}
