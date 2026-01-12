using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Indexer.Model;

public class Document
{
  [Name("Regal")]
  [JsonPropertyName("archive_rack")]
  public string ArchiveRack { get; set; } = string.Empty;

  [Name("Ordner-nummer")]
  [JsonPropertyName("archive_folder")]
  public string ArchiveFolder { get; set; } = string.Empty;

  [Name("Dokument-nummer")]
  [JsonPropertyName("archive_document")]
  public string ArchiveDocument { get; set; } = string.Empty;

  [Name("Sigle")]
  [JsonPropertyName("id")]
  public string Id { get; set; } = string.Empty;

  [Name("Titel des Ordners")]
  [JsonPropertyName("archive_folder_name")]
  public string ArchiveFolderName { get; set; } = string.Empty;

  [Name("Texttyp")]
  [JsonPropertyName("text_type")]
  public string Texttype { get; set; }

  [Name("Titel des Dokuments")]
  [JsonPropertyName("title")]
  public string Title { get; set; }

  [Name("enthält…")]
  [JsonIgnore]
  public string ContentText { get; set; }

  [Name("Verfasser")]
  [JsonIgnore]
  public string PersonsAuthorText { get; set; }

  [Name("Empfänger")]
  [JsonIgnore]
  public string PersonRecipientsText { get; set; }

  [Name("Genannte Personen")]
  [JsonIgnore]
  public string PersonsReferencedText { get; set; }

  [Name("Genannte Institutionen")]
  [JsonIgnore]
  public string ReferencedCompaniesText { get; set; }

  [Name("nimmt Bezug auf… (ID)")]
  [JsonIgnore]
  public string ReferencedDocumentsText { get; set; }

  [Ignore]
  [JsonPropertyName("content")]
  public string[] Content => ConvertStringToArray(ContentText);

  [Ignore]
  [JsonPropertyName("persons_author")]
  public string[] PersonsAuthor => ConvertStringToArray(PersonsAuthorText);

  [Ignore]
  [JsonPropertyName("persons_recipient")]
  public string[] PersonRecipients => ConvertStringToArray(PersonRecipientsText);

  [Ignore]
  [JsonPropertyName("persons_referenced")]
  public string[] PersonsReferenced => ConvertStringToArray(PersonsReferencedText);

  [Ignore]
  [JsonPropertyName("referenced_companies")]
  public string[] ReferencedCompanies => ConvertStringToArray(ReferencedCompaniesText);

  [Ignore]
  [JsonPropertyName("referenced_documents")]
  public string[] ReferencedDocuments => ConvertStringToArray(ReferencedDocumentsText);

  [Name("Zeit")]
  [JsonPropertyName("date")]
  public string Date { get; set; } = "*";

  [Ignore]
  [JsonPropertyName("datetime")]
  public DateTime DateTime
  {
    get
    {
      try
      {
        return DateTime.ParseExact(Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
      }
      catch
      {
        return DateTime.MinValue;
      }
    }
  }

  [Name("Umfang")]
  [JsonIgnore]
  public string PagesText { get; set; }

  [Ignore]
  [JsonPropertyName("pages")]
  public int Pages
  {
    get
    {
      try
      {
        return int.Parse(PagesText);
      }
      catch
      {
        return 0;
      }
    }
  }

  [Name("eingescannt")]
  [JsonIgnore]
  public string Scanned { get; set; }

  [Ignore]
  [JsonPropertyName("has_fulltext")]
  public bool HasFulltext => Scanned == "x";

  [Ignore]
  [JsonPropertyName("fulltext")]
  public string Fulltext { get; set; }

  private string[] ConvertStringToArray(string text)
    => string.IsNullOrEmpty(text) ? [] : text.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
}