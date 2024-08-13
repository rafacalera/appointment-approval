using MediatR;

namespace TimeEntryApproval.API.Application.Commands.ValidateTimeEntry
{
    public class ValidateTimeEntryCommand : IRequest
    {
        public Guid TimeEntryId { get; set; }
        public Guid UserId { get; set; }
        public int Role { get; set; }
    }
}
