namespace Model.RequestModel
{
    public class KeywordWithPaginationRequest : PaginationRequest
    {
        public string Keyword { get; set; } = string.Empty;
    }
}
