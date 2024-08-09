namespace TimeEntryApproval.API.Domain
{
    public class TimeEntry
    {
        private TimeEntry() { }

        public TimeEntry(DateOnly date, TimeOnly start, TimeOnly end, Guid taskId, Guid userId)
        {
            Date = date;
            Start = start;
            End = end;
            TaskId = taskId;
            UserId = userId;
        }
        public Guid TaskId { get; private set; }
        public Guid UserId { get; private set; }

        public Guid Id { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeOnly Start { get; private set; }
        public TimeOnly End { get; private set; }
        public bool? IsApproved { get; private set; }

        public void Validate(bool? isApproved)
        {
            IsApproved = isApproved;
        }
    }

}
