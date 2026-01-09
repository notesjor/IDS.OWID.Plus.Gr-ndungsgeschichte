using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Analysis;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Clients.Elasticsearch.TransformManagement;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Indexer
{
  internal class Program
  {
    private static string _indexName = "gruendungsgeschichte";
    private static CreateIndexResponse _index;
    private static ElasticsearchClient _client;

    static void Main(string[] args)
    {
      EnsureIndex();

      ProcessCompany();
    }

    private static void EnsureIndex()
    {
      _client = new ElasticsearchClient();
      if (_client.Indices.Exists(_indexName).Exists)
        _client.Indices.Delete(_indexName);
      _index = _client.Indices.Create(_indexName);

      var create = _client.Indices.CreateAsync(new CreateIndexRequest(_indexName + "_company")
      {
        Settings = new IndexSettings
        {
          MaxResultWindow = 1000000
        },
        Mappings = new TypeMapping
        {
          Properties = new Properties
          {
            { "name", new KeywordProperty() },
            { "city", new KeywordProperty() },
            { "country", new KeywordProperty() },
          }
        }
      });
      create.Wait();

      create = _client.Indices.CreateAsync(new CreateIndexRequest(_indexName + "_person")
      {
        Settings = new IndexSettings
        {
          MaxResultWindow = 1000000
        },
        Mappings = new TypeMapping
        {
          Properties = new Properties
          {
            { "id", new KeywordProperty() },
            { "key", new KeywordProperty() },
            { "lbl", new TextProperty
              {
                Analyzer = "german_custom",
                SearchAnalyzer = "german_custom",
                Fields = new Properties
                {
                  { "icu", new IcuCollationProperty
                    {
                      Language = "de",
                      Country = "DE",
                      Strength = IcuCollationStrength.Primary,
                      Alternate = IcuCollationAlternate.NonIgnorable,
                      CaseFirst = IcuCollationCaseFirst.Upper,
                    }
                  }
                }
              }
            },
            { "url", new KeywordProperty() },
            { "dic", new KeywordProperty() }
          }
        }
      });
      create.Wait();

      create = _client.Indices.CreateAsync(new CreateIndexRequest(_indexName + "_docs")
      {
        Settings = new IndexSettings
        {
          Analysis = new IndexSettingsAnalysis
          {
            Analyzers = new Analyzers
            {
              {
                "german_custom", new CustomAnalyzer
                {
                  Tokenizer = "standard",
                  Filter = ["lowercase", "german_normalization", "german_stemmer", "german_stop"]
                }
              }
            },
            TokenFilters = new TokenFilters
            {
              { "german_stop", new StopTokenFilter { Stopwords = new[] { "_german_" } } },
              { "german_stemmer", new StemmerTokenFilter { Language = "light_german" } },
            },
          },
          MaxResultWindow = 1000000
        },
        Mappings = new TypeMapping
        {
          Properties = new Properties
          {
            { "id", new KeywordProperty() },
            { "key", new KeywordProperty() },
            { "lbl", new TextProperty
              {
                Analyzer = "german_custom",
                SearchAnalyzer = "german_custom",
                Fields = new Properties
                {
                  { "icu", new IcuCollationProperty
                    {
                      Language = "de",
                      Country = "DE",
                      Strength = IcuCollationStrength.Primary,
                      Alternate = IcuCollationAlternate.NonIgnorable,
                      CaseFirst = IcuCollationCaseFirst.Upper,
                    }
                  }
                }
              }
            },
            { "url", new KeywordProperty() },
            { "dic", new KeywordProperty() }
          }
        }
      });
      create.Wait();
    }

    private static void ProcessCompany()
    {
      var data = ReadCsv<Company>("data/list_company.tsv");

      
    }

    private class Person
    {
      [Name("Person")]
      [JsonPropertyName("id")]
      public string Id { get; set; }

      [Name("Stadt")]
      [JsonPropertyName("city")]
      public string City { get; set; }

      [Name("Stadt (Ländercode)")]
      [JsonPropertyName("country")]
      public string Country { get; set; }
    }

    private class Company
    {
      [Name("Institution")]
      [JsonPropertyName("name")]
      public string Name { get; set; }

      [Name("Stadt")]
      [JsonPropertyName("city")]
      public string City { get; set; }

      [Name("Stadt (Ländercode)")]
      [JsonPropertyName("country")]
      public string Country { get; set; }
    }

    private static IEnumerable<T> ReadCsv<T>(string path)
    {
      var config = new CsvConfiguration(CultureInfo.InvariantCulture)
      {
        HasHeaderRecord = false,
      };
      using (var reader = new StreamReader(path))
      using (var csv = new CsvReader(reader, config))
        return csv.GetRecords<T>();
    }
  }
}
