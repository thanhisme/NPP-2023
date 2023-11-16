using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities
{
    [Table("NPP_Assignments")]
    public class NPPAssignment : BaseEntity
    {
        public string Description { get; set; }

        public string Note { get; set; }

        public string State { get; set; } = "in-progress";

        public string Assignee { get; set; }

        public Guid AssigneeId { get; set; }

        public string Project { get; set; }

        public Guid ProjectId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? FinishDate { get; set; }
    }
}
