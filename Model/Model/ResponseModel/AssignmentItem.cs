namespace Model.ResponseModel
{
    public class AssignmentItem
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public string State { get; set; }

        public string Project { get; set; }

        public Guid ProjectId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? FinishDate { get; set; } = null;

        public string Creator { get; set; }

        public Guid CreatorId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
