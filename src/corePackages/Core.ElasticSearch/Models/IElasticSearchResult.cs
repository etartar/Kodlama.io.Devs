namespace Core.ElasticSearch.Models
{
    public interface IElasticSearchResult
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
}
