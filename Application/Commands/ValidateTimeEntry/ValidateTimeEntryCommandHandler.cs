using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeEntryApproval.API.Application.Validator;
using TimeEntryApproval.API.Domain;
using TimeEntryApproval.API.Infrastructure;

namespace TimeEntryApproval.API.Application.Commands.ValidateTimeEntry
{
    public class ValidateTimeEntryCommandHandler : IRequestHandler<ValidateTimeEntryCommand>
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly DbSet<TimeEntry> _entity;
        private readonly TimeEntryValidator _validator;

        public ValidateTimeEntryCommandHandler(TimeEntryValidator validator, ApplicationDataContext dataContext)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _entity = _dataContext.Set<TimeEntry>();
        }

        public async Task Handle(ValidateTimeEntryCommand request, CancellationToken cancellationToken)
        {
            var timeEntry = _entity.Include(_ => _.Task).ThenInclude(_ => _.Project).FirstOrDefault(_ => _.Id == request.TimeEntryId);
            
            if (timeEntry == null)
                ArgumentNullException.ThrowIfNull(timeEntry, nameof(timeEntry));

            if (!await _validator.EvaluateRulesAsync(new TimeEntryValidation(timeEntry.Date, timeEntry.Start, timeEntry.End, timeEntry.TaskId, request.UserId, request.Role, timeEntry.Task.Project.Id, timeEntry.Task.Project.ProjectTypeId)))
                return;

            timeEntry.Approve();
            await _dataContext.SaveChangesAsync();
        }
    }
}
