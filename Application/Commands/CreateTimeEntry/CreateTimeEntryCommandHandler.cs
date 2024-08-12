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
        private readonly TimeEntryValidator _validator;

        public CreateTimeEntryCommandHandler(ApplicationDataContext dataContext, IdentityService identityService, TimeEntryValidator validator)
        {
            _ = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _identityAccount = identityService.GetAccount();
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _entity = _dataContext.Set<TimeEntry>();
        }

        public async Task<bool> Handle(CreateTimeEntryCommand request, CancellationToken cancellationToken)
        {
            var timeEntry = new TimeEntry(request.Date, request.Start, request.End, request.TaskId, _identityAccount.Id);

            _entity.Add(timeEntry);
            timeEntry.Validate(await _validator.EvaluateRulesAsync(new TimeEntryValidation(timeEntry.Date, timeEntry.Start, timeEntry.End, timeEntry.TaskId, _identityAccount.Id, _identityAccount.Role)));

            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
