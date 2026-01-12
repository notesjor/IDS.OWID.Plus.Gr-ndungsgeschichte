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
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 0.5rem;">
      Kurzbeschreibung: Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti?
    </v-col>
  </v-row> 
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 0.5rem;">
      Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti? Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti?
    </v-col>
  </v-row> 
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 0.5rem;">
      Kurzbeschreibung: Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti?
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