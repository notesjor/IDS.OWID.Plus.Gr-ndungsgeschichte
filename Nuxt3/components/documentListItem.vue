<template>
  <div style="margin:12px 10px">
    <v-row>
      <span style="text-align: left;">
        <span style="font-weight:200">[{{ formattedId }}]:&nbsp;</span>
        <span class="btn" @click="switchToDocument(data.id)">
          {{ data.title }}
        </span>
      </span>
    </v-row>
    <v-row>
      <span style="margin-left: auto; margin-right: auto;">
        <span class="btn" style="margin-top:6px; font-size:0.9rem; font-weight:200" @click="applyFilter('archive_folder_name', data.archive_folder_name)">
          [Ordner: {{ data.archive_folder_name }}]
        </span>
        <personCounter :source="data" style="margin-left:12px; display: inline-block;" />
      </span>
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
  methods: {
    switchToDocument(id) {
      this.$emit('switch-to-document', id);
    },
    applyFilter(key, value) {
      const params = new URLSearchParams(window.location.search);
      params.set(key, value);
      const newUrl = `/search?${params.toString()}`;
      window.history.pushState({}, '', newUrl);
      window.location.reload();
    }
  }
}
</script>

<style scoped>
.btn {
  border-bottom: 1px dotted #000;
  background-color: white;
  cursor: pointer;
  margin: 0px 10px 15px 0px;
  text-align: left;
}

.btn:hover {
  background-color: rgb(229, 231, 235);
}
</style>