namespace Model.RequestModel
{
    public class CreateProjectRequest
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public DateTime DueDate { get; set; } = DateTime.UtcNow;

        public DateTime? FinishDay { get; set; }

        //public IList<Guid> Members { get; set; }
    }
}
