namespace TimeEntryApproval.API.Application.Models.Requests
{
    public class CreateTimeEntryRequest
    {
        public DateOnly Date { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public Guid TaskId { get; set; }
    }
}
