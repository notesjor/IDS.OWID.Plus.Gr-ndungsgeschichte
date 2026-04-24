<template>
  <div style="margin:0px 0px -3px -5px">
    <v-tooltip :text="label_author" location="top" v-if="this.number_authors > 0" style=" z-index:999999">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-file-sign" style="margin:0px 1px;" variant="text">
          <span class="btn" @click="search('persons_author', source.id)">{{ number_authors }}</span>
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip :text="label_recipient" location="top" v-if="this.number_recipients > 0" style=" z-index:999999">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-email-newsletter" style="margin:0px 1px" variant="text">
          <span class="btn" @click="search('persons_recipient', source.id)">{{ number_recipients }}</span>        
        </v-chip>
      </template>
    </v-tooltip>

    <v-tooltip :text="label_referenced" location="top" v-if="this.number_referenced > 0" style=" z-index:999999">
      <template v-slot:activator="{ props }">
        <v-chip v-bind="props" prepend-icon="mdi-comment-account-outline" style="margin:0px 1px" variant="text">
          <span class="btn" @click="search('persons_referenced', source.id)">{{ number_referenced }}</span>          
        </v-chip>
      </template>
    </v-tooltip>
  </div>
</template>

<script>
export default {
  name: 'personCounterSmall',
  props: {
    source: {
      type: Object,
      default: {}
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
    label_author() {
      return "verfasste Dokumente";
    },
    label_recipient() {
      return "wurde in Dokumenten adressiert";
    },
    label_referenced() {
      return "wurde in Dokumenten erwähnt";
    }
  },

   methods: {
    search(key, value) {
      const params = new URLSearchParams(window.location.search);
      params.set(key, value);
      const newUrl = `/search?${params.toString()}`;
      window.history.pushState({}, '', newUrl);
      window.location.reload();
    }
  },
}
</script>

<style scoped>
.btn {
  border-bottom: 1px dotted #000;
  background-color: white;
  cursor: pointer;
  text-align: left;
}

.btn:hover {
  background-color: rgb(229, 231, 235);
}
</style>