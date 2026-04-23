<template>
  <div style="margin:2px 0px 0px -8px; display: block; width: 100%; color:#666;">
    <v-tooltip text="Jahr (Erstellung)" location="top">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-calendar-range" variant="text" style="font-size: 0.8rem;">
          {{ source.date?.substring(0, 4) }}
        </v-chip>
      </template>
    </v-tooltip>
    <v-tooltip text="Sigle (Regal/Ordner/Dokument-Nr.)" location="top">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-file-cabinet" variant="text" style="font-size: 0.8rem;">
          {{ source.id?.replace("_", "/")?.replace("_", "/") }}
        </v-chip>
      </template>
    </v-tooltip>
        <v-tooltip text="Anzahl-Seiten" location="top">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-book-open-page-variant-outline" variant="text" style="font-size: 0.8rem;">
          {{ source.pages == 0 ? "?" : source.pages }}
        </v-chip>
      </template>
    </v-tooltip>
    <v-tooltip text="Texttyp" location="top">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-shape-outline" variant="text" style="font-size: 0.8rem;">
          {{ source.text_type }}
        </v-chip>
      </template>
    </v-tooltip>
  </div>
  <div style="margin:2px 0px -10px -8px; display:block; width: 100%; color:#666">
    <v-tooltip :text="label_author" location="bottom" v-if="this.number_authors > 0" style=" z-index:999999">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-file-sign" style="margin:0px 1px; font-size: 0.8rem;" variant="text">
          {{ number_authors }}
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip :text="label_recipient" location="bottom" v-if="this.number_recipients > 0" style=" z-index:999999">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-email-newsletter" style="margin:0px 1px; font-size: 0.8rem;" variant="text">
          {{ number_recipients }}
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip :text="label_referenced" location="bottom" v-if="this.number_referenced > 0" style=" z-index:999999">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-comment-account-outline" style="margin:0px 1px; font-size: 0.8rem;" variant="text">
          {{ number_referenced }}
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip text="Anzahl referenzierter Institutionen" location="bottom" v-if="this.number_companys > 0"
      style=" z-index:999999">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-domain" style="margin:0px 1px; font-size: 0.8rem;" variant="text">
          {{ number_companys }}
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip text="Anzahl referenzierter Dokumente" location="bottom" v-if="this.number_documents > 0"
      style=" z-index:999999">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-link-box-variant-outline" style="margin:0px 1px; font-size: 0.8rem;" variant="text">
          {{ number_documents }}
        </v-chip>
      </template>
    </v-tooltip>
  </div>
</template>

<script>
export default {
  name: 'StackedPaperImage',
  props: {
    source: {
      type: Object,
      default: {}
    },
    mode: {
      type: String,
      default: "default"
    }
  },
  data() {
    return {
      dialog: false
    }
  },
  computed: {
    number_authors() {
      return this.source?.docs_author == undefined ? this.source.persons_author?.length : this.source.docs_author?.length;
    },
    number_recipients() {
      return this.source?.docs_recipient == undefined ? this.source.persons_recipient?.length : this.source.docs_recipient?.length;
    },
    number_referenced() {
      return this.source?.docs_referenced == undefined ? this.source.persons_referenced?.length : this.source.docs_referenced?.length;
    },
    number_companys() {
      return this.source?.referenced_companies == undefined ? 0 : this.source.referenced_companies?.length;
    },
    number_documents() {
      return this.source?.referenced_documents == undefined ? 0 : this.source.referenced_documents?.length;
    },
    label_author() {
      return this.mode == "default" ? "Anzahl Verfasser*innen" : "verfasste Dokumente";
    },
    label_recipient() {
      return this.mode == "default" ? "Anzahl Adressat*innen" : "wurde in Dokumenten adressiert";
    },
    label_referenced() {
      return this.mode == "default" ? "Anzahl sonstiger refernzierter Personen" : "wurde in Dokumenten erwähnt";
    }
  }
}
</script>
