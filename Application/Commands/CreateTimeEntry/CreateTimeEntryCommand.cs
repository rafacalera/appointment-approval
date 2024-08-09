using MediatR;

namespace TimeEntryApproval.API.Application.Commands.CreateTimeEntry
{
    public class CreateTimeEntryCommand : IRequest<bool>
    {
        public DateOnly Date { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public Guid TaskId { get; set; }
    }
}
