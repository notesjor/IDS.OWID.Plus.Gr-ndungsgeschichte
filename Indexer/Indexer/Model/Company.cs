using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace Indexer.Model;

public class Company
{
  [Name("Name")]
  [JsonPropertyName("name")]
  public string Name { get; set; }

  [Name("WIKIDATA-ID")]
  [JsonPropertyName("wiki_id")]
  public string WikiId { get; set; }

  [Name("WIKIDATA-URL")]
  [JsonPropertyName("wiki_url")]
  public string WikiUrl { get; set; }

  [Name("GND-Kennung")]
  [JsonPropertyName("gnd_id")]
  public string GndId { get; set; }

  [Name("GND-URL")]
  [JsonPropertyName("gnd_url")]
  public string GndUrl { get; set; }

  [Name("Stadt")]
  [JsonPropertyName("city")]
  public string City { get; set; }

  [Ignore]
  [JsonPropertyName("docs")]
  public string[] Documents { get; set; }
}