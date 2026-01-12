<template>
  <v-row style="margin:2px">
    <v-chip prepend-icon="mdi-email-fast-outline" style="margin:0px 1px;" variant="outlined" v-if="this.number_authors > 0">
      {{ number_authors }}
    </v-chip>
    <v-chip prepend-icon="mdi-email-newsletter" style="margin:0px 1px" variant="outlined" v-if="this.number_recipients > 0">
      {{ number_recipients }}
    </v-chip>
    <v-chip prepend-icon="mdi-comment-account-outline" style="margin:0px 1px" variant="outlined" v-if="this.number_referenced > 0">
      {{ number_referenced }}
    </v-chip>
    <v-chip prepend-icon="mdi-domain" style="margin:0px 1px" variant="outlined" v-if="this.number_referenced > 0">
      {{ number_referenced }}
    </v-chip>
    <v-chip prepend-icon="mdi-link-box-variant-outline" style="margin:0px 1px" variant="outlined" v-if="this.number_referenced > 0">
      {{ number_referenced }}
    </v-chip>
  </v-row>

  <!-- <v-dialog max-width="800" v-model="dialog">
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
  </v-dialog>-->
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
  computed: {
    number_authors() {
      return this.source.persons_author.length;
    },
    number_recipients() {
      return this.source.persons_recipient.length;
    },
    number_referenced() {
      return this.source.persons_referenced.length;
    },
    number_companys() {
      return this.source.referenced_companies.length;
    },
    number_documents() {
      return this.source.referenced_documents.length;
    }
  }
}
</script>
