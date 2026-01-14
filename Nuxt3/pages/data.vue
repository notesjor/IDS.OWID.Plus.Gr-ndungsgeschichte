<template>
  <projectHeader />
  <div style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 0.5rem;">
    <v-row>
      <v-col>
        Das Korpus zur Gründungsgeschichte des IDS (Leibniz-Institut für Deutsche Sprache) umfasst alle Texte, die im
        Zuge
        des Digitalisierungsprojekts erfasst wurden. Durch den Einsatz von Optical Character Recognition (OCR) konnten
        die
        historischen Dokumente digitalisiert und für die Forschung zugänglich gemacht werden. Dabei traten typische
        OCR-Fehler auf, wie falsch erkannte Buchstaben und Zeichen, diese wurden manuell nicht nachkorrigiert.
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        Für die Texterkennung kam die Software <a href="https://github.com/tesseract-ocr/tesseract">Tesseract OCR</a>,
        unterstützt durch <a href="https://github.com/DanBloomberg/leptonica">Leptonica</a>, zum Einsatz. Im Anschluss
        wurden die Texte mithilfe des
        <a href="https://notes.jan-oliver-ruediger.de/software/corpusexplorer-overview/">CorpusExplorer</a> und des
        <a href="https://www.cis.uni-muenchen.de/~schmid/tools/TreeTagger/">TreeTagger</a> bearbeitet, aufbereitet und
        annotiert. Diese
        Schritte umfassen die Tokenisierung sowie die Kennzeichnung von Satzgrenzen, Wortarten (POS) und
        Lemma-Informationen. Die finalen Texte wurden in gängige Korpus-Formate exportiert.
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        Alle Daten stehen unter der
        <a href="https://creativecommons.org/licenses/by-nc-sa/4.0/legalcode.txt">"CC BY-NC-SA 4.0"-Lizenz</a>
        und können daher kostenfrei zur nicht-kommerziellen Forschung und Lehre genutzt werden. Falls Sie die
        Daten nutzen, zitieren Sie bitte das Projekt entsprechend wissenschaftlicher Standards.
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <p style="font-size: 1.25rem; font-weight: 200;">Zitationsvorschläge:</p>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <div style="font-weight: 300;">
          Projektpublikation
        </div>
        <div style="margin: 0px 10px 20px 10px; font-size: 0.85rem; font-style: italic;">
          Scholl, Stefan / Waßmer, Lea / Dang Anh, Mark / Müller-Spitzer, Carolin (in Vorbereitung): Die Frühzeit des
          Instituts für Deutsche Sprache – Einblicke aus der Erschließung des Vorstandsarchivs. In: Deutsche Sprache.
          Zeitschrift für Theorie, Praxis, Dokumentation (?), S. ?
        </div>
        <div style="font-weight: 300;">
          Datensatz
        </div>
        <div style="margin: 0px 10px 20px 10px; font-size: 0.85rem; font-style: italic;">
          Waßmer, Lea / Rüdiger, Jan Oliver / Scholl, Stefan / Dang Anh, Mark / Müller-Spitzer, Carolin (2026):
          IDS-Gründungsgechichte - Erschließung des Vorstandsarchivs. https://owid.de/plus/idsgg/data/.
        </div>
        <div style="font-weight: 300;">
          Webapplikation
        </div>
        <div style="margin: 0px 10px 20px 10px; font-size: 0.85rem; font-style: italic;">
          Rüdiger, Jan Oliver / Waßmer, Lea / Scholl, Stefan / Dang Anh, Mark / Müller-Spitzer, Carolin (2026):
          IDS-Gründungsgechichte - Webapplikation. https://owid.de/plus/idsgg/.
        </div>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <p style="font-size: 1.25rem; font-weight: 200;">Folgende Daten stehen zum Download bereit:</p>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <div v-for="x in items" :key="x" class="item">
          <a :href="x.url" target="_blank">
            <v-row>
              <v-col cols="1" style="text-align: right;">
                <v-icon>mdi-download</v-icon>
              </v-col>
              <v-col cols="11">
                <span>{{ x.name }}</span>&nbsp;
                <span style="font-weight: 200;">({{ x.size }} {{ x.unit }})</span>
              </v-col>
            </v-row>
            <v-row style="margin:-25px 0px 10px -10px; font-size: 0.75rem;">
              <v-col cols="1"></v-col>
              <v-col cols="11">
                <span>{{ x.desc }}</span>
              </v-col>
            </v-row>
          </a>
        </div>
      </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      items: [
        { name: 'TSV-Liste verknüpfter Institute', size: '7', unit: 'KB', url: 'data/list_company.tsv', desc: 'Die Liste umfasst neben dem Namen auch die Stadt, sowie GND/Wikidata-IDs' },
        { name: 'TSV-Liste verknüpfter Personen', size: '34', unit: 'KB', url: 'data/list_persons.tsv', desc: 'Die Basis-Daten zu den verknüpften Personen, inkl. GND/Wikidata-IDs' },
        { name: 'TSV-Liste digitalisierter Dokumente', size: '126', unit: 'KB', url: 'data/list_documents.tsv', desc: 'Diese Liste ist die Datengrundlage für die Funktion: "Findebuch"' },
        { name: 'Digitalisate (PDF-Scans)', size: '301', unit: 'MB', url: 'data/Digitalisate_PDF.zip', desc: 'PDF-Dokumente der digitalisierten Dokumente - nur Bilddaten' },
        { name: 'Digitalisate (TXT-Dateien)', size: '522', unit: 'KB', url: 'data/Digitalisate_TXT.zip', desc: 'OCR-Erkannter Plaintext (Leptonica / Tesseract OCR) - Rohtext' },
        { name: 'Korpus (verschiedene Formate)', size: '10', unit: 'MB', url: 'data/KorpusDaten.zip', desc: 'Korpusdaten in den Formaten: I5, KorAP, CorpusExplorer, CoNLL, TCF, CWB, VRT, TreeTagger, TXM, WebLicht' }
      ],
    }
  },
}
</script>

<style scoped>
a {
  text-decoration: none;
  border-bottom: 1px dotted #000;
  background-color: white;
}

a:hover {
  text-decoration: none;
  border-bottom: 1px dotted #000;
  background-color: rgba(211, 211, 211, 0.2);
}

div.item {
  background-color: white;
}

div.item:hover {
  background-color: rgba(211, 211, 211, 0.2);
}
</style>