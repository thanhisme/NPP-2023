namespace Model.RequestModel
{
    public class AssignmentFilterRequest : PaginationRequest
    {
        private static readonly DateTime _currentDate = DateTime.UtcNow;
        public DateTime StartDate { get; set; } = GetFirstDayOfMonth(_currentDate);

        public DateTime EndDate { get; set; } = GetLastDayOfMonth(_currentDate);

        public Guid? Department { get; set; }

        public Guid? Project { get; set; }

        private static DateTime GetFirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        private static DateTime GetLastDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }
    }
}
