namespace Entity;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid CreatorId { get; set; }

    public string Creator { get; set; }

    public Guid? ModifierId { get; set; }

    public string Modifier { get; set; }

    public bool IsDeleted { get; set; }
}