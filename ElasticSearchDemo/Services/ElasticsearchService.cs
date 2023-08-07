using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearchDemo.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;

namespace ElasticSearchDemo.Services
{
    public class ElasticsearchService
    {
        private readonly ILogger<ElasticsearchService> _logger;
        private readonly ElasticsearchClient elasticsearchClient;
        public ElasticsearchService(ILogger<ElasticsearchService> logger)
        {
            var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
            elasticsearchClient = new ElasticsearchClient(settings);
            _logger = logger;
        }

        public async Task TestLogging()
        {
            // index = "logs-index-1"
            _logger.LogInformation("I test elasticsearch");

            var x = 2;

            if (x == 2)
                _logger.LogWarning("x = 2");
        }

        public async Task<string> CreateIndex(CreateIndexDto createIndexDto)
        {
            var settings = await elasticsearchClient.Indices.CreateAsync(createIndexDto.Name,
                index => index.Settings(
                    s => s.Index(index => index
                    .NumberOfReplicas(createIndexDto.NumberOfReplicas)
                    .NumberOfShards(createIndexDto.NumberOfShards)
            )));

            if (!settings.IsValidResponse)
                return $"Create index {createIndexDto.Name} failed.";
            
            return $"Create index {createIndexDto.Name} succeeded.";
        }

        public async Task<string> AddDocument<T>(CreateDocument<T> createDocumentDto)
        {
            var response = await elasticsearchClient.IndexAsync(createDocumentDto.Document, createDocumentDto.IndexName);

            if (!response.IsValidResponse)
                return $"Create document failed.";
            
            return $"Create document succeeded.";
        }

        public async Task<IReadOnlyCollection<T>> SearchDocument<T>(SearchKeyword searchKeyword)
        {
            var fields = string.IsNullOrEmpty(searchKeyword.FieldName) ? "*" : searchKeyword.FieldName;

            var request = new SearchRequest(searchKeyword.IndexName)
            {
                From = 0,
                Size = 10,
                Query = new MultiMatchQuery
                {
                    Query = searchKeyword.Value,
                    Type = TextQueryType.PhrasePrefix,
                    Fields = fields
                }
            };

            var response = await elasticsearchClient.SearchAsync<T>(request);

            foreach (var hit in response.Hits)
                

            if (!response.IsValidResponse)
                return new List<T>();

            return response.Documents;
        }

        public async Task<string> DeleteDocument(DeleteDocumentDto deleteDocumentDto)
        {
            
            var response = await elasticsearchClient.DeleteAsync(deleteDocumentDto.IndexName, deleteDocumentDto.DocumentId);

            if (!response.IsValidResponse)
                return $"Delete document {deleteDocumentDto.DocumentId} failed.";

            return $"Delete document {deleteDocumentDto.DocumentId} succeeded.";
        }
    }
}
