<script setup>
useHead({
  title: "IDS‑Gründungsgeschichte: Startseite"
})
</script>

<template>
  <projectHeader />
  <v-row style="max-width:80ch; margin-left:auto; margin-right:auto;">
    <v-col>
      Das hier zur Verfügung gestellte Spezialarchiv umfasst Dokumente aus
      der Gründungs- und Frühphase des Instituts für Deutsche Sprache (IDS)
      aus dem Zeitraum 1963 bis 1971. Die Digitalisate stammen aus dem
      Vorstandsarchiv des IDS und wurden 2024/2025 im Rahmen einer Erhebung
      zur <a href="/plus/idsgg/team">Gründungsgeschichte des IDS</a> erfasst und annotiert.
    </v-col>
  </v-row>
  <v-row style="max-width:80ch; margin-left:auto; margin-right:auto;">
    <v-col>
      Die vorliegende Archiverfassung bietet die Möglichkeit, die komplexen
      personellen und institutionellen Beziehungsgeflechte der
      IDS-Gründungsphase auf einer systematisch erschlossenen, transparent
      dokumentierten Datengrundlage zu rekonstruieren. Bei der
      Zusammenstellung wurden nur Dokumente aus dem Gründungszeitraum
      berücksichtigt, die von besonderer inhaltlicher Relevanz im Hinblick auf
      die Existenzargumente, die Finanzierung und die nationale und
      internationale (Selbst-)Positionierung des Instituts sind.
    </v-col>
  </v-row>


  <v-row style="max-width:80ch; margin-left:auto; margin-right:auto;">
    <v-col cols="4">
      <a href="/plus/idsgg/search" style="cursor: pointer;">
        <div
          style="border:1px #ccc solid; border-radius: 5px; padding: 10px; background-color: rgba(0, 0, 0, 0.05); margin-top:10px">
          <div style="text-align: center;">
            <v-icon style="font-size: 60px;">
              mdi-text-box-search-outline
            </v-icon>
            <div><strong>Findbuch</strong><br />Durchsuchen Sie alle digitalisierten Texte und lassen Sie sich
              Metadaten und Volltext anzeigen.</div>
          </div>
        </div>
      </a>
    </v-col>
    <v-col cols="4">
      <a href="/plus/idsgg/browse" style="cursor: pointer;">
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
      <a href="/plus/idsgg/data" style="cursor: pointer;">
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

<style lang="scss" scoped>
a {
  border-bottom: 1px dotted #000;
}
</style>