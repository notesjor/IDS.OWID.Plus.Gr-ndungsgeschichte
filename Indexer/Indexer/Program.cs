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
using Indexer.Model;
using System.Text;

namespace Indexer
{
  internal class Program
  {
    private static string _indexName = "gruendungsgeschichte";
    private static ElasticsearchClient _client;

    static void Main(string[] args)
    {
      var docs = ReadCsv<Document>("data/list_documents.tsv");
      docs = FixIndexNumbers(docs);

      EnsureIndex();

      ProcessCompany(docs);
      ProcessPerson(docs);
      ProcessDocument(docs);
    }

    private static Document[] FixIndexNumbers(Document[] docs)
    {
      for (var i = 0; i < docs.Length; i++)
      {
        if (docs[i].ArchiveRack.Length == 1)
          docs[i].ArchiveRack = $"0{docs[i].ArchiveRack}";
        if (docs[i].ArchiveFolder.Length == 1)
          docs[i].ArchiveFolder = $"0{docs[i].ArchiveFolder}";
        if (docs[i].ArchiveDocument.Length == 1)
          docs[i].ArchiveDocument = $"0{docs[i].ArchiveDocument}";

        docs[i].Id = $"{docs[i].ArchiveRack}_{docs[i].ArchiveFolder}_{docs[i].ArchiveDocument}";
      }
      return docs;
    }

    private static void EnsureIndex()
    {
      _client = new ElasticsearchClient();
      if (_client.Indices.Exists($"{_indexName}_company").Exists)
      {
        _client.Indices.Delete($"{_indexName}_company");
        _client.Indices.Delete($"{_indexName}_person");
        _client.Indices.Delete($"{_indexName}_doc");
      }

      var create = _client.Indices.CreateAsync(new CreateIndexRequest($"{_indexName}_company")
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
            { "wiki_id", new KeywordProperty{ Index = false } },
            { "gnd_id", new KeywordProperty{ Index = false } },
            { "wiki_url", new KeywordProperty{ Index = false } },
            { "gnd_url", new KeywordProperty{ Index = false } },
            { "docs" , new KeywordProperty() }
          }
        }
      });
      create.Wait();

      create = _client.Indices.CreateAsync(new CreateIndexRequest($"{_indexName}_person")
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
            { "name_family", new KeywordProperty() },
            { "name_given", new KeywordProperty() },
            { "wiki_id", new KeywordProperty{ Index = false } },
            { "gnd_id", new KeywordProperty{ Index = false } },
            { "wiki_url", new KeywordProperty{ Index = false } },
            { "gnd_url", new KeywordProperty{ Index = false } },
            { "year_birth", new KeywordProperty{ Index = false } },
            { "year_death", new KeywordProperty{ Index = false } },
            { "gender", new KeywordProperty() },
            { "info", new KeywordProperty{ Index = false } },
            { "docs_author" , new KeywordProperty() },
            { "docs_recipient" , new KeywordProperty() },
            { "docs_referenced" , new KeywordProperty() },
          }
        }
      });
      create.Wait();

      create = _client.Indices.CreateAsync(new CreateIndexRequest($"{_indexName}_doc")
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
            { "archive_rack", new KeywordProperty() },
            { "archive_folder", new KeywordProperty() },
            { "archive_document", new KeywordProperty() },
            { "archive_folder_name", new KeywordProperty() },
            { "text_type", new KeywordProperty() },
            { "title", new KeywordProperty() },
            { "content", new KeywordProperty() },
            { "persons_author", new KeywordProperty() },
            { "persons_recipient", new KeywordProperty() },
            { "persons_referenced", new KeywordProperty() },
            { "referenced_companies", new KeywordProperty() },
            { "referenced_documents", new KeywordProperty() },
            { "date", new KeywordProperty() },
            { "datetime", new DateProperty() },
            { "pages", new IntegerNumberProperty() },
            { "has_fulltext", new BooleanProperty() },
            { "fulltext", new TextProperty
              {
                Analyzer = "german_custom",
                SearchAnalyzer = "german_custom"
              }
            }
          }
        }
      });
      create.Wait();
    }

    private static void ProcessCompany(IEnumerable<Document> docs)
    {
      var data = ReadCsv<Company>("data/list_company.tsv").ToArray();

      for (var i = 0; i < data.Length; i++)
      {
        var name = data[i].Name;
        data[i].Documents = (from d in docs where d.ReferencedCompanies.Any(x => x == name) select d.Id).ToArray();
      }

      _client.IndexMany(data, $"{_indexName}_company");
    }

    private static void ProcessPerson(IEnumerable<Document> docs)
    {
      var data = ReadCsv<Person>("data/list_persons.tsv").ToArray();

      for (var i = 0; i < data.Length; i++)
      {
        var name = data[i].Id;
        data[i].DocsAuthor = (from d in docs where d.PersonsAuthor.Any(x => x == name) select d.Id).ToArray();
        data[i].DocsRecipient = (from d in docs where d.PersonRecipients.Any(x => x == name) select d.Id).ToArray();
        data[i].DocsReferenced = (from d in docs where d.PersonsReferenced.Any(x => x == name) select d.Id).ToArray();
      }

      _client.IndexMany(data, $"{_indexName}_person");
    }

    private static void ProcessDocument(Document[] docs)
    {
      var basePath = "I:/IDS - Gründungsgeschichte/Simple";

      for (var i = 0; i < docs.Length; i++)
      {
        try
        {
          if (!docs[i].HasFulltext)
            continue;

          var path = $"{basePath}/{docs[i].Id}.txt";
          docs[i].Fulltext = File.ReadAllText(path, Encoding.UTF8);
        }
        catch
        {
          // ignore
        }
      }

      _client.IndexMany(docs, $"{_indexName}_doc");
    }

    private static T[] ReadCsv<T>(string path)
    {
      var config = new CsvConfiguration(CultureInfo.InvariantCulture)
      {
        HasHeaderRecord = true,
        Delimiter = "\t",
        NewLine = "\r\n"
      };
      using (var reader = new StreamReader(path))
      using (var csv = new CsvReader(reader, config))
        return csv.GetRecords<T>().ToArray();
    }
  }
}

