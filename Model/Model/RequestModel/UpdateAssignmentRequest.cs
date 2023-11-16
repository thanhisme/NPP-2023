namespace Model.RequestModel
{
    public class UpdateAssignmentRequest
    {
        public string? Description { get; set; }

        public string? Note { get; set; }

        public Guid? AssigneeId { get; set; }

        public Guid? ProjectId { get; set; }

        public DateTime? StartDate { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public string? State { get; set; }
    }
}
