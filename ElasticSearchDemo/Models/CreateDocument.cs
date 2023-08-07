namespace ElasticSearchDemo.Models
{
    public sealed record CreateDocument<T>(T Document, string IndexName);
}
