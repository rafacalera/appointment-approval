using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeEntryApproval.API.Application.Commands.ValidateTimeEntry;
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
        private readonly IMediator _mediator;

        public CreateTimeEntryCommandHandler(ApplicationDataContext dataContext, IdentityService identityService, TimeEntryValidator validator, IMediator mediator)
        {
            _ = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _identityAccount = identityService.GetAccount();
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _entity = _dataContext.Set<TimeEntry>();
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(CreateTimeEntryCommand request, CancellationToken cancellationToken)
        {
            var timeEntry = new TimeEntry(request.Date, request.Start, request.End, request.TaskId, _identityAccount.Id);

            _entity.Add(timeEntry);

            if (await _dataContext.SaveChangesAsync() <= 0)
                return false;

            await _mediator.Send(new ValidateTimeEntryCommand { TimeEntryId = timeEntry.Id, Role = _identityAccount.Role, UserId = _identityAccount.Id});

            return true;
        }
    }
}
