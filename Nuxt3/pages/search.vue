<template>
  <projectHeader />
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 2rem;">
      Kurzbeschreibung: Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti?
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="3" style="padding: 0px 30px 0px 20px;">
      <v-expansion-panels v-model="panels" multiple>
        <searchBox title="xyz" rkey="xyz" :items="search?.localSearch?.list?.archive_rack" />
      </v-expansion-panels>
    </v-col>
    <v-col cols="9" style="margin:-15px 0px 0px -15px">

      <div style="display:flex; flex-wrap:wrap; gap:0.75rem; align-items:flex-start;">
        <div v-for="x in esResults" :key="x">
          <paperstack :source="x._source" @callback="showItemDialog" />
        </div>
      </div>
    </v-col>
  </v-row>

  <v-dialog max-width="800" v-model="dialog">
    <v-card title="Dialog">
      <v-card-text>
        <div>
          <img :src="src" alt="Bild auf oberstem Blatt"
            style="max-height: 400px; max-width: 480px; padding: 10px; float: left;" />
          <i>Regal:</i> {{ dialogData.archive_rack }} / <i>Ordner:</i> {{ dialogData.archive_folder }} /
          <i>Ordner-Name:</i> {{ dialogData.archive_folder_name }}<br />
          <i>Dokument-Nr. im Ordner:</i> {{ dialogData.archive_document }}<br />
          <i>Text-Typ:</i> {{ dialogData.text_type }}<br />
          <br />
          <i>Kategorie:</i> {{ category }}<br />
          <i>Jahr:</i> {{ year }}<br />
          <i>Seitenanzahl:</i> {{ pages }}<br />
          <i>Titel:</i> {{ dialogData.titel }}<br /><br />
          <i>Inhalts-Schlagwörter:</i><br />
          <div>
            <v-chip v-for="x in dialogData.content" :key="x" style="margin:2px">{{ x }}</v-chip>
          </div>
          <i>Autor:</i> {{ author }}<br />
        </div>
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn text="Ok" @click="isActive.value = false"></v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
//import esQuick from "../api/esQuick.js";
import { useSearchStore } from '~/stores/search';

export default {
  data() {
    return {
      search: null,

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

    this.search = useSearchStore();
    this.search.initialize();
    //this.esQuick = new esQuick();
    //this.esQuick.initialize("http://localhost:9200", "gruendungsgeschichte_doc");
    this.refresh();
  },

  methods: {
    refresh() {
      var self = this;

      this.esResults = self.search.getCurrentData().hits.hits;

      /*
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
      */
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