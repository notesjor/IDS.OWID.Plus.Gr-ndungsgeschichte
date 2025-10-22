<template>
  <v-row style="max-width:80ch; margin-left:auto; margin-right:auto; padding-top: 1rem;">
    <v-col>
      <div style="margin-left: auto; margin-right: auto;">
        <img src="/stack_small.png" alt="papierstapel" width="150" style="margin: -0.75rem 0px 0px 0px; float: left;" />
        <h1>IDS-Gründungsgeschichte</h1>
        <h3 style="margin:-10px 0px 0px 0px">Hier Projekt Slogan/Slack-Line einfügen...</h3>
      </div>
    </v-col>
  </v-row>
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
        <v-expansion-panel title="Jahr" value="foo">
          <v-expansion-panel-text>
            <v-range-slider v-model="search_dateRange" :max="1970" :min="1945" :step="1" thumb-label="always"
              style="margin-top:25px"></v-range-slider>
          </v-expansion-panel-text>
        </v-expansion-panel>

        <v-expansion-panel title="Autor" value="bar">
          <v-expansion-panel-text>
            <div style="margin:0px -20px 0px -35px;">
        <v-row>
          <v-col>
            <div style="font-size:12px; display:block; float:left; padding-left:25px">
              Auswahl:
            </div>
            <a @click="selectAll" style="cursor: pointer;">
              <h6 style="font-size:12px; display:block; float:left; margin-left:10px; font-variant:small-caps">
                Alle
              </h6>
            </a>
            <a @click="selectNone" style="cursor: pointer;">
              <h6 style="font-size:12px; display:block; float:left; margin:0px 10px 0px 10px; font-variant:small-caps">
                Keine
              </h6>
            </a>
            <a @click="selectInvert" style="cursor: pointer;">
              <h6 style="font-size:12px; display:block; float:left; font-variant:small-caps">
                Invertieren
              </h6>
            </a>
          </v-col>
        </v-row>
        <v-row style="padding:0px 20px 0px 20px">
          <v-text-field v-model="search" :label="boxName + ' suchen...'" single-line hide-details
            style="padding:0px 15px 0px 15px"></v-text-field>
        </v-row>
        <v-row style="padding-bottom:10px">
          <!-- Tabelle mit Footer / wenn Suche -->
          <v-data-table :headers="headers" :items="inputItems" :sort-by="['item']" show-select item-value="item"
            :search="search" item-class="no-divider" style="margin:10px 17px 10px 35px"
            :items-per-page=5 :items-per-page-options="[
              { value: 5, title: '5' },
              { value: 10, title: '10' },
              { value: 25, title: '25' },
              { value: 50, title: '50' },
              { value: 100, title: '100' },
              { value: -1, title: '$vuetify.dataFooter.itemsPerPageAll' }
            ]" no-data-text="Keine Daten verfügbar. Bitte probieren Sie eine andere Abfrage!"
            :pageText="'{0}-{1} von {2}'" items-per-page-text="Pro Seite:">
            <template v-slot:item.item="{ item }">
              <span style="color:darkgray; font-size: 0.8em;">
                {{ item.item }}
              </span>
            </template>
            <template v-slot:item.freq="{ item }">
              <span style="font-weight:bold; color:darkgray; font-size: 0.8em;">
                <!-- ({{item.freq }}) -->
              </span>
            </template>
          </v-data-table>
        </v-row>
      </div>
      <v-alert v-if="note != null" icon="mdi-information-outline" :text="note"
        style="font-size: 14px; margin-top:-25px; font-family: var(--FF-DISPLAY)"></v-alert>
          </v-expansion-panel-text>
        </v-expansion-panel>

        <v-expansion-panel></v-expansion-panel>
      </v-expansion-panels>
    </v-col>
    <v-col cols="9" style="margin:-15px 0px 0px -15px">
      <div style="display:flex; flex-wrap:wrap; gap:0.75rem; align-items:flex-start;">
        <papertack src="/ggesch/dummy01.jpg" year="1945" author="Arnsberg" pages="5" category="Urkunde" />
        <papertack src="/ggesch/dummy02.jpg" year="1963" author="Müller" pages="7" category="Bericht" />
        <papertack src="/ggesch/dummy03.jpg" year="1978" author="Wolfberger" pages="4" category="Brief" />
        <papertack src="/ggesch/dummy04.jpg" year="1955" author="Hruodgêr" pages="3" category="Aktennotiz" />
        <papertack src="/ggesch/dummy01.jpg" year="1948" author="Schmidt" pages="5" category="Bericht" />
        <papertack src="/ggesch/dummy02.jpg" year="1966" author="Müller" pages="7" category="Bericht" />
        <papertack src="/ggesch/dummy03.jpg" year="1981" author="Wolfberger" pages="4" category="Bericht" />
        <papertack src="/ggesch/dummy02.jpg" year="1933" author="Müller" pages="7" category="Bericht" />
        <papertack src="/ggesch/dummy04.jpg" year="1951" author="Hruodgêr" pages="3" category="Aktennotiz" />
        <papertack v-for="i in 100" :key="i"></papertack>
      </div>
    </v-col>
  </v-row>
</template>

<script>
export default {
  data() {
    return {
      appName: 'Nuxt 3',
      appDescription: 'The Intuitive Vue Framework',
      panels: [],
      search_dateRange: [1945, 1970],
      inputItems: [
        { item: 'Autor A'},
        { item: 'Autor B'},
        { item: 'Autor C'},
        { item: 'Autor D'},
      ],
    }
  },
  mounted() {
    this.appName = this.$config.public.appName;
    this.appDescription = this.$config.public.appDescription;
  },
}
</script>

<style lang="scss" scoped></style>