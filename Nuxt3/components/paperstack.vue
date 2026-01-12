<template>
  <v-card style="max-width: 300px; max-height: 455px;" @click="dialog = true">
    <v-card-text>
      <v-row>
        <div style="margin-right: auto;">
          <v-chip prepend-icon="mdi-calendar-range">
            {{ source.date }}
          </v-chip>
        </div>
        <div style="margin-left: auto;">
          <v-chip prepend-icon="mdi-file-cabinet">
            {{ source.id.replace("_", "/").replace("_", "/") }}
          </v-chip>
        </div>
      </v-row>
      <v-row style="margin-top:13px">
        <div style="margin-right: auto;">
          <v-chip prepend-icon="mdi-shape-outline">
            {{ source.text_type }}
          </v-chip>
        </div>
        <div style="margin-left: auto;">
          <v-chip prepend-icon="mdi-book-open-page-variant-outline">
            {{ source.pages }}
          </v-chip>
        </div>
      </v-row>
      <!--
      <v-row>
        <div style="margin-left: auto; margin-right: auto;">
          <img :src="src" alt="Bild auf oberstem Blatt" v-if="src != ''"
            style="min-height: 200px; max-height: 200px; max-width: 280px; padding: 10px;" />
          <v-icon v-else style="min-height: 200px; font-size: 128px;">mdi-note-text-outline</v-icon>
        </div>
      </v-row>
    -->
      <v-row>
        <div style="width: 100%; text-align: center; font-weight: 200; font-size: 1.1rem; margin-top: 0.5rem; margin-bottom: 0.5rem;">
          {{ source.title }}
        </div>
      </v-row>
      <v-row>
        <div style="margin-right: auto; margin-left:auto">
          <personCounter :source="source"/>
        </div>        
      </v-row>

    </v-card-text>
  </v-card>
  <!--
  <v-dialog max-width="800" v-model="dialog">
    <v-card title="Dialog">
      <v-card-text>
        <div>
          <img :src="src" alt="Bild auf oberstem Blatt"
            style="max-height: 400px; max-width: 480px; padding: 10px; float: left;" />
            <i>Regal:</i> 12<br/>
            <i>Ordner:</i> B3<br/>
            <i>Dokument im Ordner:</i> 5<br/><br/>
            <i>Kategorie:</i> {{ category }}<br/>
            <i>Jahr:</i> {{ year }}<br/>
            <i>Seitenanzahl:</i> {{ pages }}<br/>
            <i>Autor:</i> {{ author }}<br/>
            <i>Titel:</i> {{ titel }}<br/><br/>
            <i>Beschreibung:</i><br/>
            Aufgaben und Ziele; Erforschung der gegenwärtigen Sprache; Untersuchung der Grammatik; Erstellung einer inhaltsbezogenen Grammatik; Erstellung einer strukturalistischen Grammatik; Untersuchung der gesprochenen Sprache; Untersuchung der Sprache in Mittel- und Ostdeutschland; Koordination; IDS in Nachfolge früherer Bestrebungen; Rolle des Forschungsschwerpunkts Sprache und Gemeinschaft im IDS # Organisationsstruktur; Gründungsmitglieder des IDS # Unterstützung; Förderung durch Fritz-Thyssen-Stiftung; Förderung durch Werner Schütz; Förderung durch Hans Reschke # Beziehung zu anderen Forschungsinstitutionen; Beziehung zum Institut für deutsche Sprache und Literatur Ost-Berlin; Beziehung zum Deutschen Sprachatlas; Beziehung zur Duden-Redaktion # Verhältnis zum Ausland; Verhältnis zur DDR <br/><br/>
            <i>Personen:</i><br/>
            Grebe, Paul; Hensen, Walter; Hotzenköcherle, Rudolf; Klein, Karl Kurt; Maurer, Friedrich; Moser, Hugo; Weisgerber, Leo; Schütz, Werner; Reschke, Hans<br/><br/>
            <i>Institutionen:</i><br/>
            Institut für deutsche Sprache und Literatur an der Akademie der Wissenschaften; Deutscher Sprachatlas; Forschungsschwerpunkt Sprache und Gemeinschaft; Deutsche Forschungsgemeinschaft; Duden-Redaktion; Kurfürstliche Deutsche Gesellschaft; Fritz-Thyssen-Stiftung
        </div>
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn text="Ok" @click="isActive.value = false"></v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
-->
</template>

<script>
export default {
  name: 'StackedPaperImage',
  props: {
    source: {
      type: Object,
      default: {}
    },
  },
  data() {
    return {
      dialog: false
    }
  },
  mounted() {
  }
}
</script>
