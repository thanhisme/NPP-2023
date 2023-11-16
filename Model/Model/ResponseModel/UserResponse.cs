namespace Model.ResponseModel
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
