<template>
  <v-card>
    <v-card-text style="max-width: 100ch;">
      <v-row>
        <i>Regal:&nbsp;</i> {{ dialogData.archive_rack }} /&nbsp;<i>Ordner:&nbsp;</i> {{ dialogData.archive_folder }}
        /&nbsp;<i>Dokument-Nr.:&nbsp;</i> {{ dialogData.archive_document }} /&nbsp;<i>Text-Typ:&nbsp;</i>{{ dialogData.text_type }}
      </v-row>
      <v-row>
        <span><i>Titel (Ordner):&nbsp;</i>{{ dialogData.archive_folder_name }}</span>
      </v-row>
      <v-row>
        <span><i>Titel (Dokument):&nbsp;</i>{{ dialogData.title }}</span>
      </v-row>
      <v-row>
        <i>Datum:&nbsp;</i> {{ dialogData.date }} /&nbsp;<i>Anzahl Seiten:&nbsp;</i> {{ dialogData.pages }}
      </v-row>
      <v-row>
        <v-col cols="3">
          <img :src="`thumbnail/${dialogData.id}.jpg`" alt="Bild auf oberstem Blatt"
            style="max-height: 300px; max-width: 480px; margin-left:auto; margin-right: auto;" />
          <v-btn prepend-icon="mdi-download-circle-outline" style="margin:5px 0px"
            v-if="dialogData.has_fulltext"
            :href="`fulltext/${dialogData.id}.pdf`">
            Download</v-btn>
        </v-col>
        <v-col cols="1"></v-col>
        <v-col cols="8">
          <i>Schlagwörter (Inhalt):</i><br />
          <div>
            <v-chip v-for="x in dialogData.content" :key="x" style="margin:2px">{{ x }}</v-chip>
          </div>
        </v-col>
      </v-row>
      <v-row>
        <v-expansion-panels variant="accordion">
          <v-expansion-panel v-if="dialogData.persons_author?.length > 0">
            <v-expansion-panel-title>
              <v-icon>mdi-email-fast-outline</v-icon>
              <span style="margin:0px 5px">Verfasser*innen</span>
              <span>(Anzahl: {{ dialogData.persons_author?.length }})</span>
            </v-expansion-panel-title>
            <v-expansion-panel-text>
              <personList :data="dialogData.persons_author"/>
            </v-expansion-panel-text>
          </v-expansion-panel>
          <v-expansion-panel v-if="dialogData.persons_recipient?.length > 0">
            <v-expansion-panel-title>
              <v-icon>mdi-email-newsletter</v-icon>
              <span style="margin:0px 5px">Adressat*innen</span>
              <span>(Anzahl: {{ dialogData.persons_recipient?.length }})</span>
            </v-expansion-panel-title>
            <v-expansion-panel-text>
              <personList :data="dialogData.persons_recipient"/>
            </v-expansion-panel-text>
          </v-expansion-panel>
          <v-expansion-panel v-if="dialogData.persons_referenced?.length > 0">
            <v-expansion-panel-title>
              <v-icon>mdi-comment-account-outline</v-icon>
              <span style="margin:0px 5px">Sonstige refernzierte Personen</span>
              <span>(Anzahl: {{ dialogData.persons_referenced?.length }})</span>
            </v-expansion-panel-title>
            <v-expansion-panel-text>
              <personList :data="dialogData.persons_referenced"/>
            </v-expansion-panel-text>
          </v-expansion-panel>
          <v-expansion-panel v-if="dialogData.referenced_companies?.length > 0">
            <v-expansion-panel-title>
              <v-icon>mdi-domain</v-icon>
              <span style="margin:0px 5px">Referenzierte Institutionen</span>
              <span>(Anzahl: {{ dialogData.referenced_companies?.length }})</span>
            </v-expansion-panel-title>
            <v-expansion-panel-text>
              <companyList :data="dialogData.referenced_companies"/>
            </v-expansion-panel-text>
          </v-expansion-panel>
          <v-expansion-panel v-if="dialogData.referenced_documents?.length > 0">
            <v-expansion-panel-title>
              <v-icon>mdi-link-box-variant-outline</v-icon>
              <span style="margin:0px 5px">Referenzierte Dokumente</span>
              <span>(Anzahl: {{ dialogData.referenced_documents?.length }})</span>
            </v-expansion-panel-title>
            <v-expansion-panel-text>
              <documentList :data="dialogData.referenced_documents"/>
            </v-expansion-panel-text>
          </v-expansion-panel>
        </v-expansion-panels>
      </v-row>
    </v-card-text>

    <v-card-actions>
      <v-spacer></v-spacer>

      <v-btn text="Ok" @click="isActive.value = false"></v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import esQuick from "../api/esQuick.js";

export default {
  data() {
    return {
      esQuick: null,
      esQuery: "*",
      esPageCurrent: 0,
      esPageSize: 500,
      esPageMax: 100,
      esResults: [],

      dialog: false,
      dialogData: {},

      appName: 'Nuxt 3',
      appDescription: 'The Intuitive Vue Framework',
      panels: [],
      search_dateRange: [1945, 1970],
      inputItems: [
        { item: 'Autor A' },
        { item: 'Autor B' },
        { item: 'Autor C' },
        { item: 'Autor D' },
      ],
    }
  },
  mounted() {
    this.appName = this.$config.public.appName;
    this.appDescription = this.$config.public.appDescription;

    this.esQuick = new esQuick();
    this.esQuick.initialize("http://localhost:9200", "gruendungsgeschichte_doc");
    this.showItemDialog("04_02_01");
  },

  methods: {
    refresh() {
      var self = this;
      this.esQuick.easySearch(this.esQuery, this.esPageCurrent, this.esPageSize, (json) => {
        self.esPageMax = self.esQuick.easySearchGetMax(json);
        self.esResults = self.esQuick.easySearchGetHits(json);
        self.esResults.forEach(item => {
          delete item._source.fulltext;
          try {
            item._source.date = item._source.date.substring(0, 4);
          } catch {
            // ignore
          }
        });
      });
    },
    showItemDialog(id) {
      var self = this;
      this.esQuick.getDocumentById(id, (json) => {
        self.dialogData = json._source;
        self.dialog = true;
      });
    }
  }
}
</script>

<style lang="scss" scoped></style>