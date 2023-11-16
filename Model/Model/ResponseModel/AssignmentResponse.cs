namespace Model.ResponseModel
{
    public class AssignmentResponse
    {
        public Guid AssigneeId { get; set; }

        public string Assignee { get; set; }

        public List<AssignmentItem> assignments { get; set; }
    }
}
