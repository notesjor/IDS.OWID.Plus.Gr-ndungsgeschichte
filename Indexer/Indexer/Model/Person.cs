using CsvHelper.Configuration.Attributes;
using System.Text.Json.Serialization;

namespace Indexer.Model
{
  public class Person
  {
    [Name("Nachname")]
    [JsonPropertyName("name_family")]
    public string NameFamily { get; set; } = string.Empty;

    [Name("Vorname")]
    [JsonPropertyName("name_given")]
    public string NameGiven { get; set; } = string.Empty;
    
    private string _id = string.Empty;

    [Ignore]
    [JsonPropertyName("id")]
    public string Id
    {
      get
      {
        if (string.IsNullOrEmpty(_id))
          _id = string.IsNullOrEmpty(NameGiven) ? NameFamily : $"{NameFamily}, {NameGiven}";
        return _id;
      }
      set => _id = value;
    }

    [Name("WIKIDATA-ID")]
    [JsonPropertyName("wiki_id")]
    public string WikiId { get; set; }

    [Name("WIKIDATA-URL")]
    [JsonPropertyName("wiki_url")]
    public string WikiUrl { get; set; }

    [Name("GND-ID")]
    [JsonPropertyName("gnd_id")]
    public string GndId { get; set; }

    [Name("GND-URL")]
    [JsonPropertyName("gnd_url")]
    public string GndUrl { get; set; }

    [Name("Geburtsjahr")]
    [JsonPropertyName("year_birth")]
    public string YearBirth { get; set; } = "?";

    [Name("Sterbejahr")]
    [JsonPropertyName("year_death")]
    public string YearDeath { get; set; } = "*";

    [Name("Geschlecht")]
    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [Name("Informationen zur Person")]
    [JsonPropertyName("info")]
    public string Info { get; set; }

    [Ignore]
    [JsonPropertyName("docs_author")]
    public string[] DocsAuthor { get; set; }

    [Ignore]
    [JsonPropertyName("docs_recipient")]
    public string[] DocsRecipient { get; set; }

    [Ignore]
    [JsonPropertyName("docs_referenced")]
    public string[] DocsReferenced { get; set; }
  }
}
