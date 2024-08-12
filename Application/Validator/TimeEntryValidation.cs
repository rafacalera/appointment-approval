namespace TimeEntryApproval.API.Application.Validator
{
    public class TimeEntryValidation
    {
        public TimeEntryValidation(DateOnly date, TimeOnly start, TimeOnly end, Guid taskId, Guid userId, int role, Guid projectId, int projectTypeId)
        {
            Date = date;
            Start = start;
            End = end;
            TaskId = taskId;
            UserId = userId;
            Role = role;
            ProjectId = projectId;
            ProjectTypeId = projectTypeId;
        }

        public DateOnly Date { get; private set; }
        public TimeOnly Start { get; private set; }
        public TimeOnly End { get; private set; }
        public Guid TaskId { get; private set; }
        public Guid ProjectId { get; private set; }
        public int ProjectTypeId { get; private set; }
        public Guid UserId { get; private set; }
        public int Role { get; private set; }
    }
}
