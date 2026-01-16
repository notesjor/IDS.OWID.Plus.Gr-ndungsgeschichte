<template>
  <projectHeader />
  <v-row style="max-width:80ch; margin-left:auto; margin-right:auto;">
    <v-col>
      Kurzbeschreibung: Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti?
    </v-col>
  </v-row>
  <v-row style="max-width:80ch; margin-left:auto; margin-right:auto;">
    <v-col>
      Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti? Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit
      quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti?
    </v-col>
  </v-row>
  <v-row style="max-width:80ch; margin-left:auto; margin-right:auto;">
    <v-col>
      Kurzbeschreibung: Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti?
    </v-col>
  </v-row>

  <v-row style="max-width:80ch; margin-left:auto; margin-right:auto;">
    <v-col cols="4">
      <a href="/search" style="cursor: pointer;">
        <div
          style="border:1px #ccc solid; border-radius: 5px; padding: 10px; background-color: rgba(0, 0, 0, 0.05); margin-top:10px">
          <div style="text-align: center;">
            <v-icon style="font-size: 60px;">
              mdi-text-box-search-outline
            </v-icon>
            <div><strong>Findebuch</strong><br />Durchsuchen Sie alle digitalisierten Texte und lassen Sie sich
              Metadaten und Volltext anzeigen.</div>
          </div>
        </div>
      </a>
    </v-col>
    <v-col cols="4">
      <a href="/browse" style="cursor: pointer;">
        <div
          style="border:1px #ccc solid; border-radius: 5px; padding: 10px; background-color: rgba(0, 0, 0, 0.05); margin-top:10px">
          <div style="text-align: center;">
            <v-icon style="font-size: 60px;">
              mdi-file-cabinet
            </v-icon>
            <div><strong>Verzeichnis</strong><br />Im Verzeichnis finden Sie alle Institutionen, Personen und
              Schlagwörter.</div>
          </div>
        </div>
      </a>
    </v-col>
    <v-col cols="4">
      <a href="/download" style="cursor: pointer;">
        <div
          style="border:1px #ccc solid; border-radius: 5px; padding: 10px; background-color: rgba(0, 0, 0, 0.05); margin-top:10px">
          <div style="text-align: center;">
            <v-icon style="font-size: 60px;">
              mdi-download
            </v-icon>
            <div><strong>Download</strong><br />Alle Daten stehen zum Download für die nicht-kommerzielle Forschung und
              Lehre zur Verfügung.</div>
          </div>
        </div>
      </a>
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
  },
}
</script>

<style lang="scss" scoped></style>