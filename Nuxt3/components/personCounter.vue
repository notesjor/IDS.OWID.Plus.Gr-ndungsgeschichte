<template>
  <v-row style="margin:2px">
    <v-tooltip :text="label_author" location="top" v-if="this.number_authors > 0">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-file-sign" style="margin:0px 1px;" variant="text">
          {{ number_authors }}
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip :text="label_recipient" location="top" v-if="this.number_recipients > 0">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-email-newsletter" style="margin:0px 1px" variant="text">
          {{ number_recipients }}
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip :text="label_referenced" location="top" v-if="this.number_referenced > 0">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-comment-account-outline" style="margin:0px 1px" variant="text">
          {{ number_referenced }}
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip text="Anzahl referenzierter Institutionen" location="top" v-if="this.number_companys > 0">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-domain" style="margin:0px 1px" variant="text">
          {{ number_companys }}
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip text="Anzahl referenzierter Dokumente" location="top" v-if="this.number_documents > 0">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-link-box-variant-outline" style="margin:0px 1px" variant="text">
          {{ number_documents }}
        </v-chip>
      </template>
    </v-tooltip>
  </v-row>
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
