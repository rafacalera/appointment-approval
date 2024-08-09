namespace TimeEntryApproval.API.Application.Validator
{
    public class TimeEntryValidation
    {
        public DateOnly Date { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public int Role { get; set; }
    }
}
