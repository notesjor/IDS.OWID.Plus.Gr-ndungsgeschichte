<template>
  <div style="max-width: 80ch; margin-left: auto; margin-right: auto;">
    <h1>Beteiligte Personen und Projekte</h1>
    <div>
        <h2>Entwicklungsteam für das <hi>Syntagmatikon</hi>
        </h2>
        <v-list>
            <v-list-item>
                <NuxtLink to="https://www.ids-mannheim.de/lexik/personal/steyer">Dr. Kathrin Steyer</NuxtLink>
            </v-list-item>
            <v-list-item>
                <NuxtLink to="https://perso.ids-mannheim.de/seiten/brunner.html">Dr. Annelen Brunner</NuxtLink>
            </v-list-item>
            <v-list-item>
                <NuxtLink to="https://perso.ids-mannheim.de/seiten/ruediger.html">Dr. Jan Oliver Rüdiger</NuxtLink>
            </v-list-item>
        </v-list>
        <div>Logo-Design von Tom Steyer <NuxtLink to="https://erit-lux.events">https://erit-lux.events
            </NuxtLink>
        </div>

        <h2>Informationen zu den integrierten Ressourcen</h2>
        <h3>Verantwortliche Personen</h3>

        <v-row class="d-flex align-center">
            <v-col cols=""> <resources-list-compact
                    :filter="['PREPCON', 'SpruchList', 'SPRW', 'WVBF', 'FesteWV', 'DTWW']" /></v-col>
            <v-col cols="6">
                <NuxtLink to="https://www.ids-mannheim.de/lexik/personal/steyer">Dr. Kathrin Steyer</NuxtLink>
            </v-col>

        </v-row>
        <v-row class="d-flex align-center">
            <v-col><resources-list-compact :filter="['KoMuX', 'PhrasKomp']" /></v-col>
            <v-col cols="6">
                <NuxtLink to="https://perso.ids-mannheim.de/seiten/hein.html">Dr. Katrin Hein-Antonioli</NuxtLink>
            </v-col>

        </v-row>

        <v-row class="d-flex align-center">
            <v-col>
                <r rkey="MAP" />
            </v-col>
            <v-col cols="6">
                <NuxtLink to="https://perso.ids-mannheim.de/seiten/proost.html">Dr. Kristel Proost</NuxtLink><br />
                <NuxtLink to="https://perso.ids-mannheim.de/seiten/zeschel.html">Dr. Arne Zeschel</NuxtLink>
            </v-col>

        </v-row>

        <v-row class="d-flex align-center">
            <v-col>
                <r rkey="Verlaufsformen" />
            </v-col>
            <v-col cols="6">
                <NuxtLink to="https://www.ids-mannheim.de/lexik/personal/engelberg">Prof. Dr. Stefan Engelberg
                </NuxtLink>
            </v-col>

        </v-row>
        <v-row class="d-flex align-center">
            <v-col>
                <r rkey="Redeeinleiter" />
            </v-col>
            <v-col cols="6">
                <NuxtLink to="https://perso.ids-mannheim.de/seiten/tu.html">Dr. Ngoc Duyen Tanja Tu</NuxtLink><br />
                <NuxtLink to="https://perso.ids-mannheim.de/seiten/brunner.html">Dr. Annelen Brunner</NuxtLink>
            </v-col>

        </v-row>

        <h3>Projekte und Arbeitsbereiche</h3>

        <p>Informationen zu den Projekten und Arbeitsbereichen, in am <hi>Syntagmatikon</hi> beteiligt sind, finden Sie auf folgenden Webseiten:</p>
        <!-- <p style="border-top: solid #C0C0C0 1px; padding-top: 10px"> -->
        <p style="padding-top: 10px">
            <NuxtLink to="https://www.ids-mannheim.de/lexik/syntagmatik-im-lexikon/">Projekt „Syntagmatik im
                Lexikon“</NuxtLink>
        </p>
        <p style="margin-left:40px">
            <NuxtLink to="https://www.ids-mannheim.de/lexik/uwv/">Usuelle Wortverbindungen</NuxtLink>
        </p>
        <p style="margin-left:40px">
            <NuxtLink to="https://www.ids-mannheim.de/lexik/verben-und-argumentstrukturen/">Argumentstrukturen
            </NuxtLink>
        </p>
        <p style="margin-left:40px">
            <NuxtLink to="https://www.ids-mannheim.de/lexik/syntagmatik-im-lexikon/wortbildungsmuster/">
                Wortbildungsmuster </NuxtLink>
        </p>
        <p style="padding-top: 10px">
            <NuxtLink
                to="https://www.ids-mannheim.de/lexik/pb-lexik-empirisch-digital/lexikologische-informationssysteme/">
                Projekt „Lexikologische Informationssysteme“</NuxtLink>
        </p>
        <p style="padding-top: 10px">
            <NuxtLink to="https://www.ids-mannheim.de/lexik/redewiedergabe/">Projekt „Redewiedergabe“</NuxtLink>
            (abgeschlossen)
        </p>
    </div>
  </div>
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