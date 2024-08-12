namespace TimeEntryApproval.API.Domain
{
    public class ProjectTask
    {
        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public Project Project { get; }
    }
}
