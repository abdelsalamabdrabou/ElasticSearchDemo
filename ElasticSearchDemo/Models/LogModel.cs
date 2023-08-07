namespace ElasticSearchDemo.Models
{
    public sealed record LogModel(
        DateTime @Timestamp,
        string Level,
        string Message,      
        FieldsModel Fields);
}
