using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities
{
    [Table("NPP_Projects")]
    public class NPPProject : BaseEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProjectManager { get; set; }

        public Guid ProjectManagerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public string State { get; set; } = "in-progress";
    }
}
