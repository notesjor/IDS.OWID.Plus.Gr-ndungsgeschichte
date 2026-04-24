<template>
  <span>{{ k }}</span>:&nbsp;

  <v-tooltip text="Dokumente mit Bezug zu diesem Schlagwort" location="top">
    <template v-slot:activator="{ props }">
      <span v-bind="props" @click="search(k)" class="btn"><v-icon>mdi-text-box-search-outline</v-icon>
        <span>{{ v }}</span></span>
    </template>
  </v-tooltip>
</template>

<script>
export default {
  props: {
    k: {
      type: String,
    },
    v: {
      type: Number
    }
  },

  methods: {
    search(value) {
      const params = new URLSearchParams(window.location.search);
      params.set("content", value);
      const newUrl = `/search?${params.toString()}`;
      window.history.pushState({}, '', newUrl);
      window.location.reload();
    }
  },

  computed: {
  }
}
</script>

<style scoped>
.btn {
  border-bottom: 1px dotted #000;
  background-color: white;
  cursor: pointer;
  font-weight: 200;
  text-align: left;
}

.btn:hover {
  background-color: rgb(229, 231, 235);
}
</style>