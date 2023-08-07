namespace ElasticSearchDemo.Models
{
    public sealed record CreateIndexDto(string Name, int NumberOfReplicas, int NumberOfShards);
}
