using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeEntryApproval.API.Application.Security;
using TimeEntryApproval.API.Application.Validator;
using TimeEntryApproval.API.Domain;
using TimeEntryApproval.API.Infrastructure;

namespace TimeEntryApproval.API.Application.Commands.CreateTimeEntry
{
    public class CreateTimeEntryCommandHandler : IRequestHandler<CreateTimeEntryCommand, bool>
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly DbSet<TimeEntry> _entity;
        private readonly IdentityAccount _identityAccount;

        public CreateTimeEntryCommandHandler(ApplicationDataContext dataContext, IdentityService identityService)
        {
            _ = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _identityAccount = identityService.GetAccount();
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _entity = _dataContext.Set<TimeEntry>();
        }

        public async Task<bool> Handle(CreateTimeEntryCommand request, CancellationToken cancellationToken)
        {
            var timeEntry = new TimeEntry(request.Date, request.Start, request.End, request.TaskId, _identityAccount.Id);

            _entity.Add(timeEntry);
            timeEntry.Validate(await AppointmentValidator.EvaluateRulesAsync(new TimeEntryValidation()
            {
                Date = timeEntry.Date,
                Start = timeEntry.Start,
                End = timeEntry.End,
                TaskId = timeEntry.TaskId,
                UserId = timeEntry.UserId,
                Role = _identityAccount.Role
            }));

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
