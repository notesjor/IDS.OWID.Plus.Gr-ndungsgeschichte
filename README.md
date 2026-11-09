# IDS-Gründungsgeschichte

Dieses Repository enthält den Quellcode für die Webapplikation zur IDS-Gründungsgeschichte sowie ein separates Werkzeug zur Aufbereitung und Indizierung der Archivdaten.

## Zum Archiv

Das Spezialarchiv umfasst ausgewählte Dokumente aus der Gründungs- und Frühphase des Instituts für Deutsche Sprache (IDS) von 1963 bis 1971. Die Digitalisate stammen aus dem Vorstandsarchiv des IDS und wurden 2024/2025 im Rahmen einer Erhebung zur [Gründungsgeschichte des IDS](https://gruendungsgeschichte.ids-mannheim.de/team) erfasst und annotiert.

Die Archiverfassung macht personelle und institutionelle Beziehungsgeflechte auf einer systematisch erschlossenen und transparent dokumentierten Datengrundlage recherchierbar. Berücksichtigt wurden Dokumente von besonderer inhaltlicher Relevanz für die Existenzargumente und Finanzierung des Instituts sowie seine nationale und internationale (Selbst-)Positionierung.

## Bestandteile

### Indexer

Das .NET-Kommandozeilenprogramm liest die tabulatorgetrennten Dateien `list_documents.tsv`, `list_company.tsv` und `list_persons.tsv` ein. Es vereinheitlicht die Signaturen der Dokumente, ergänzt bei Personen und Institutionen die zugehörigen Dokumentverknüpfungen und erstellt daraus die Elasticsearch-Indizes `gruendungsgeschichte_doc`, `gruendungsgeschichte_person` und `gruendungsgeschichte_company`. Für Dokumente mit Volltext lädt es zusätzlich die passenden UTF-8-Textdateien und richtet die Volltextsuche mit deutscher Analyse ein.

Der Indexer ist ein Datenaufbereitungswerkzeug und nicht Teil des Nuxt-Laufzeitbetriebs. Die Nuxt-App bezieht ihre Suchdaten derzeit nicht live aus Elasticsearch: Der verwendete Dokument-, Personen- und Institutionenbestand ist im Pinia-Store der App hinterlegt. Eine Änderung der Elasticsearch-Indizes aktualisiert diesen Frontend-Datenbestand daher nicht automatisch.

**Achtung:** Wenn der Index `gruendungsgeschichte_company` bereits existiert, löscht der Indexer die drei genannten Indizes und legt sie neu an. Vor einem Lauf auf einer gemeinsam genutzten Elasticsearch-Instanz müssen vorhandene Daten gesichert und die Folgen geprüft werden.

### Nuxt3

Die Nuxt-3-Anwendung stellt das Archiv als Rechercheoberfläche bereit. Im **Findbuch** lassen sich Dokumente nach Metadaten wie Signatur, Ordner, Texttyp, Personen, Institutionen, Schlagwörtern und Zeitraum filtern. Die Detailansicht zeigt Metadaten, verfügbare Downloads sowie verknüpfte Personen, Institutionen und Dokumente in einer Visualisierung. Das **Verzeichnis** erschließt Institutionen, Personen und Schlagwörter; die **Datenseite** bietet die freigegebenen TSV-Dateien zum Download an.

Die Suche und Filterung der eingebundenen Daten erfolgen im Browser. Die Anwendung basiert unter anderem auf Vue, Vuetify, Pinia und FlexSearch.

## Voraussetzungen

- .NET SDK 10 für den Indexer
- Node.js und pnpm für Nuxt3 (im Projekt liegt eine `pnpm-lock.yaml`)
- Für den Indexlauf: eine erreichbare Elasticsearch-Instanz sowie, falls Volltexte indiziert werden sollen, die passenden `.txt`-Dateien

## Indexer ausführen

Die drei TSV-Dateien liegen unter `Indexer/Indexer/data/`. Vom Projektverzeichnis aus:

```powershell
cd Indexer/Indexer
dotnet run
```

Der Indexer erwartet die Volltextdateien standardmäßig im Verzeichnis `I:/IDS - Gründungsgeschichte/Simple`. Dieser Pfad ist derzeit im Quellcode fest eingetragen. Dokumente ohne verfügbaren Volltext werden trotzdem mit ihren Metadaten indiziert.

## Nuxt3 starten

```powershell
cd Nuxt3
pnpm install
pnpm dev
```

Der Entwicklungsserver ist anschließend üblicherweise unter <http://localhost:3000> erreichbar. Für einen Produktionsbuild:

```powershell
pnpm build
```

## Daten

Die TSV-Dateien des Indexers liegen in `Indexer/Indexer/data/`; die für Downloads und Anzeige bereitgestellten Dateien befinden sich in `Nuxt3/public/data/`. Der aktuell in der App verwendete Suchbestand liegt zusätzlich im Pinia-Store unter `Nuxt3/stores/search.js`. Diese Ablagen werden nicht automatisch miteinander synchronisiert.