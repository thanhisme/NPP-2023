using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities
{
    [Table("NPP_Departments")]
    public class NPPDepartment
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }
    }
}
