<template>
  <projectHeader />
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 2rem;">
      Das Findebuch umfasst alle Texte des IDS-Vorstandsarchivs der Jahre 1963–1970. Auf der linken Seite finden Sie
      verschiedene Facetten, über die Sie gezielt suchen können. Jeder gefundene Eintrag verweist auf wichtige Metriken
      wie Datum, Texttyp, Sigle oder Seitenanzahl. Unten in der Fußleiste jedes Eintrags sehen Sie außerdem, wie viele
      Personen oder Institutionen mit dem jeweiligen Text verknüpft sind.
      <br />
      Texte, die Bilder enthalten, sind als Volltext verfügbar. Die computergenerierten Bilder der Textstapel vermitteln
      zudem einen Eindruck davon, wie viele Seiten ein Dokument umfasst. Durch einen Klick auf einen Text erhalten Sie
      zusätzliche Detailinformationen und können auch über diese Detailinformationen gezielt suchen.
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="3" style="padding: 0px 30px 0px 20px;">
      <v-expansion-panels v-model="panels" multiple v-if="search != null">
        <searchBox @changed="reload" title="Regal-Nummer" rkey="archive_rack"/>
        <searchBox @changed="reload" title="Ordner-Name" rkey="archive_folder_name"/>
        <searchBox @changed="reload" title="Texttyp" rkey="text_type"/>
        <searchBox @changed="reload" title="Autor*in(nen)" rkey="persons_author"/>
        <searchBox @changed="reload" title="Empfänger*in(nen)" rkey="persons_recipient"/>
        <searchBox @changed="reload" title="Referenzierte Personen" rkey="persons_referenced"/>
        <searchBox @changed="reload" title="Referenzierte Institutionen" rkey="referenced_companies"/>
        <searchBox @changed="reload" title="Schlagwörter (Inhalte/Themen)" rkey="content"/>
      </v-expansion-panels>
    </v-col>
    <v-col cols="9" style="margin:-15px 0px 0px -15px">

      <div style="display:flex; flex-wrap:wrap; gap:0.75rem; align-items:flex-start;">
        <div v-for="(v, k) in esResults" :key="k">
          <paperstack :source="v" @callback="showItemDialog" />
        </div>
      </div>
    </v-col>
  </v-row>

  <v-dialog max-width="800" v-model="dialog" style="margin:75px 0px 0px 0px; z-index:99999">
    <v-card>
      <v-card-text style="max-width: 100ch; padding:40px">
        <v-row style="font-size:1.25rem; font-weight: 500;">Dokument: {{ dialogData.archive_rack }}/{{
          dialogData.archive_folder }}/{{ dialogData.archive_document }}</v-row>
        <v-row style="font-weight: 300; font-size: 0.85rem; margin-bottom: 20px;">
          <p>
            <span style="font-weight: 500;">Hinweis:&nbsp;</span>
            <span style="text-align: left;">Unterstrichene Eigenschaften, Schlagwörter und verknüpfte Entitäten können
              angeklickt
              werden, um ähnliche Dokumente zu finden.</span>
          </p>
        </v-row>
        <v-row>
          <div style="display: block; width:100%">Titel (Ordner):</div>
          <div @click="applyFilter" class="btn">{{ dialogData.archive_folder_name }}</div>
        </v-row>
        <v-row>
          <div style="display: block; width:100%">Titel (Dokument):</div>
          <div @click="applyFilter" style="font-weight: 200; margin: 0px 10px 15px 20px; text-align: left;">{{
            dialogData.title }}</div>
        </v-row>
        <v-row>
          <span>
            Texttyp:&nbsp;
            <span @click="applyFilter" class="btn" style="margin-left: 0px;">{{ dialogData.text_type }}</span>
          </span>
        </v-row>
        <v-row>
          <span>
            Datum:&nbsp;
            <span style="font-weight: 200;">
              <span @click="applyFilter" class="btn" style="margin: 0px;">{{ dialogData.date.substring(0, 4) }}</span>
              <span v-if="dialogData.date.length > 4">{{ dialogData.date.substring(4) }}</span>
            </span>
            /&nbsp;Anzahl Seiten:&nbsp;
            <span @click="applyFilter" class="btn" style="margin: 0px;">{{ dialogData.pages }}</span>
          </span>
        </v-row>
        <v-row>
          <v-col cols="3">
            <img :src="`thumbnail/${dialogData.id}.jpg`" alt="Bild auf oberstem Blatt"
              style="max-height: 300px; max-width: 480px; margin-left:auto; margin-right: auto;" />
            <v-btn prepend-icon="mdi-download-circle-outline" style="margin:5px 0px" v-if="dialogData.has_fulltext"
              :href="`fulltext/${dialogData.id}.pdf`">
              Download</v-btn>
          </v-col>
          <v-col cols="1"></v-col>
          <v-col cols="8">
            Schlagwörter (Inhalt):<br />
            <div>
              <v-chip class="btnChip" v-for="x in dialogData.content" :key="x" style="margin:2px">{{ x }}</v-chip>
            </div>
          </v-col>
        </v-row>
        <v-row style="margin-top:30px">
          Verknüpfte Entitäten:
          <v-expansion-panels variant="accordion">
            <v-expansion-panel v-if="dialogData.persons_author?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-email-fast-outline</v-icon>
                <span style="margin:0px 5px">Verfasser*innen</span>
                <span>(Anzahl: {{ dialogData.persons_author?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <personList :data="dialogData.persons_author" />
              </v-expansion-panel-text>
            </v-expansion-panel>
            <v-expansion-panel v-if="dialogData.persons_recipient?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-email-newsletter</v-icon>
                <span style="margin:0px 5px">Adressat*innen</span>
                <span>(Anzahl: {{ dialogData.persons_recipient?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <personList :data="dialogData.persons_recipient" />
              </v-expansion-panel-text>
            </v-expansion-panel>
            <v-expansion-panel v-if="dialogData.persons_referenced?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-comment-account-outline</v-icon>
                <span style="margin:0px 5px">Sonstige refernzierte Personen</span>
                <span>(Anzahl: {{ dialogData.persons_referenced?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <personList :data="dialogData.persons_referenced" />
              </v-expansion-panel-text>
            </v-expansion-panel>
            <v-expansion-panel v-if="dialogData.referenced_companies?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-domain</v-icon>
                <span style="margin:0px 5px">Referenzierte Institutionen</span>
                <span>(Anzahl: {{ dialogData.referenced_companies?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <companyList :data="dialogData.referenced_companies" />
              </v-expansion-panel-text>
            </v-expansion-panel>
            <v-expansion-panel v-if="dialogData.referenced_documents?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-link-box-variant-outline</v-icon>
                <span style="margin:0px 5px">Referenzierte Dokumente</span>
                <span>(Anzahl: {{ dialogData.referenced_documents?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <documentList :data="dialogData.referenced_documents" />
              </v-expansion-panel-text>
            </v-expansion-panel>
          </v-expansion-panels>
        </v-row>
      </v-card-text>
    </v-card>
  </v-dialog>

  <v-dialog v-model="loading" persistent>
    <div class="d-flex flex-column align-center justify-center" 
      style="width:110%; background-color: white; margin:20px 0px 20px -25px; padding: 20px 0px">
      <v-progress-circular indeterminate :size="60" class="mb-4" />
      <div>Daten werden geladen...</div>
    </div>
  </v-dialog>
</template>

<script>
import { useSearchStore } from '~/stores/search';

export default {
  data() {
    return {
      search: null,
      esResults: [],

      dialog: false,
      dialogData: {},
      panels: [],
      loading: false,
    }
  },
  mounted() {
    this.appName = this.$config.public.appName;
    this.appDescription = this.$config.public.appDescription;

    this.search = useSearchStore();
    this.search.initialize();

    // Parse URL parameters
    const params = new URLSearchParams(window.location.search);
    const query = {};

    params.forEach((value, key) => {
      if (key === 'id') {
        this.showItemDialog(value);
      } else {
        if (query[key]) {
          if (Array.isArray(query[key])) {
            query[key].push(value);
          } else {
            query[key] = [query[key], value];
          }
        } else {
          query[key] = value;
        }
      }
    });

    Object.keys(query).forEach((key) => {
      this.search.localSearch.query[key] = query[key];
    });

    this.reload();
  },

  methods: {
    reload() {
      this.loading = true;
      setTimeout(() => {
        this.esResults = this.search.getCurrentData();
        this.loading = false;
      }, 200);
    },
    showItemDialog(id) {
      this.dialogData = this.search.getById(id);
      this.dialog = true;
    },
  }
}
</script>

<style scoped>
.btn {
  border-bottom: 1px dotted #000;
  background-color: white;
  cursor: pointer;
  font-weight: 200;
  margin: 0px 10px 15px 20px;
  text-align: left;
}

.btn:hover {
  background-color: rgb(229, 231, 235);
}

.btnChip {
  background-color: rgb(255, 255, 255);
  cursor: pointer;
}

.btnChip:hover {
  background-color: rgb(229, 231, 235);
}
</style>