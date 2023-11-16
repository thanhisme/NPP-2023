using Microsoft.EntityFrameworkCore;

namespace Entity.Entities;

public partial class NPPContext : DbContext
{
    private readonly string _connectionString;
    public NPPContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public NPPContext(DbContextOptions<NPPContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NPPUser> NPPUsers { get; set; }

    public virtual DbSet<NPPDepartment> NPPDepartments { get; set; }

    public virtual DbSet<NPPProject> NPPProjects { get; set; }

    public virtual DbSet<NPPAssignment> NPPAssignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => base.OnConfiguring(optionsBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
