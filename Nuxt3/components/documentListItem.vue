<template>
  <div style="margin:12px 0px">
    <v-row>
      <span style="font-weight:200">{{ formattedId }}&nbsp;&nbsp;-&nbsp;&nbsp;{{ data.title }}</span>
    </v-row>
    <v-row>
      <span style="margin-top:6px; font-size:0.9rem; font-weight:200">{{ data.archive_folder_name }}</span>
      <personCounter :source="data" style="margin-left:12px" />
    </v-row>
  </div>
</template>

<script>
import { useSearchStore } from '~/stores/search';

export default {
  name: 'documentListItem',
  props: {
    source: {
      type: String,
      default: ''
    },
  },
  data() {
    return {
      search: null,
      data: {}
    }
  },
  computed: {
    formattedId() {
      const id = this.source || this.data.id || '';
      return id.replace(/_/g, '/');
    }
  },
  mounted() {
    this.search = useSearchStore();
    this.search.initialize();
    this.data = this.search.getById(this.source);
  },
}
</script>