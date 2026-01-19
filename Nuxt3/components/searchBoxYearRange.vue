<template>
  <v-expansion-panel>
    <v-expansion-panel-title>
      <v-icon :style="styleIcon" v-if="!allSameValue" @click="selectAll">mdi-filter-remove</v-icon>
      {{ title }}
    </v-expansion-panel-title>
    <v-expansion-panel-text>
      <v-row>
        <v-range-slider v-model="selectedRange" :min="min" :max="max" :step="1" thumb-label="always"
          :style="styleGradient" 
          style="min-width: 100%; margin:30px 0px 0px 20px; padding-right: 40px;"></v-range-slider>
      </v-row>
      <v-row>
        <v-btn @click="applyFilter" style="text-transform: none; margin-left: auto; margin-right: auto;" prepend-icon="mdi-calendar-collapse-horizontal">Filter übernehmen</v-btn>
      </v-row>
    </v-expansion-panel-text>
  </v-expansion-panel>
</template>

<style scoped>
label {
  opacity: 1 !important;
}
</style>

<script>
import { useSearchStore } from '~/stores/search';

export default {
  props: {
    title: {
      type: String,
      required: true
    },
    rkey: {
      type: String,
      required: true
    },
    color1: {
      type: String,
      default: "black"
    },
    color2: {
      type: String,
      default: "white"
    },
  },

  mounted() {
    this.search = useSearchStore();
    this.search.initialize();

    const items = this.search.$state.localSearch.list[this.rkey] || [];

    this.syncLock = true;
    this.min = items.min;
    this.max = items.max;
    this.selectedRange = [this.min, this.max];

    if(this.search.$state.localSearch.query[this.rkey] === undefined) {
      this.syncLock = false;
      return;
    }
    const query = parseInt(this.search.$state.localSearch.query[this.rkey]);
    this.selectedRange = [query, query];
    this.syncLock = false;
  },

  data() {
    return {
      search: {},
      selectedRange: [1900, 2026],
      min: 1900,
      max: 2026,
      syncLock: false
    }
  },

  methods: {
    selectAll() {
      this.selectedRange = [this.min, this.max];
      this.applyFilter();
    },
    applyFilter() {
      this.search.$state.localSearch.query[this.rkey] = { from: this.selectedRange[0], to: this.selectedRange[1] };
      this.$emit("changed");
    }
  },

  computed: {
    allSameValue() {
      return this.selectedRange[0] == this.min && this.selectedRange[1] == this.max;
    },
    styleGradient() {
      return this.allSameValue ? "margin: -17px 5px -17px -25px; max-width:10px" : "margin: -17px 10px -17px -25px; max-width: 50px "
    },
    styleIcon() {
      return `margin: 0px 15px 0px -10px; color: ${this.$props.color1};`;
    },
    paginatedItems() {
      const start = (this.currentPage - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return this.localItems.slice(start, end);
    }
  }
}
</script>