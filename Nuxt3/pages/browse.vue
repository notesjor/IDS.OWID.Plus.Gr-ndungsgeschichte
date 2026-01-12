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
    <v-col cols="2" style="padding: 0px 30px 0px 20px;">
    </v-col>
    <v-col cols="9" style="margin:-15px 0px 0px -15px">
      <v-row>
        <v-col cols="4">
          <v-card class="mx-auto" prepend-icon="mdi-domain" subtitle="Referenzierte Institution im Findebuch" width="400">
            <template v-slot:title>
              <span class="font-weight-black">Institutionen</span>
            </template>

            <v-card-text class="bg-surface-light pt-4">
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Commodi, ratione debitis quis est labore
              voluptatibus! Eaque
              cupiditate minima, at placeat totam, magni doloremque veniam neque porro libero rerum unde voluptatem!
            </v-card-text>
          </v-card>
        </v-col>
        <v-col cols="4">
          <v-card class="mx-auto" prepend-icon="mdi-account" subtitle="Beteiligte Personen im Schriftverkehr" width="400">
            <template v-slot:title>
              <span class="font-weight-black">Personen</span>
            </template>

            <v-card-text class="bg-surface-light pt-4">
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Commodi, ratione debitis quis est labore
              voluptatibus!
              Eaque cupiditate minima, at placeat totam, magni doloremque veniam neque porro libero rerum unde
              voluptatem!
            </v-card-text>
          </v-card>
        </v-col>
        <v-col cols="4">
          <v-card class="mx-auto" prepend-icon="mdi-tag-text" subtitle="zu Themen und Inhalten" width="400">
            <template v-slot:title>
              <span class="font-weight-black">Schlagworte</span>
            </template>

            <v-card-text class="bg-surface-light pt-4">
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Commodi, ratione debitis quis est labore
              voluptatibus!
              Eaque cupiditate minima, at placeat totam, magni doloremque veniam neque porro libero rerum unde
              voluptatem!
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-col>
    <v-col></v-col>
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