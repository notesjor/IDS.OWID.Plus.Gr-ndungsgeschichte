<template>
  <v-expansion-panel>
    <v-expansion-panel-title>
      <v-icon :style="styleIcon" v-if="!allSameValue" @click="selectAll">mdi-filter-remove</v-icon>
      {{ title }}
    </v-expansion-panel-title>
    <v-expansion-panel-text>
      <div class="nolink">
        <v-row style="margin:0px 0px 0px -35px;">
          <v-col>
            <div style="font-size:14px; display:block; float:left; padding-left:25px">
              Auswahl:
            </div>
            <a @click="selectAll" style="cursor: pointer; font-weight: 600;">
              <h6 style="font-size:14px; display:block; float:left; margin-left:10px; font-variant:small-caps">
                Alle
              </h6>
            </a>
            <a @click="selectNone" style="cursor: pointer; font-weight: 600;">
              <h6 style="font-size:14px; display:block; float:left; margin:0px 10px 0px 10px; font-variant:small-caps">
                Keine
              </h6>
            </a>
            <a @click="selectInvert" style="cursor: pointer; font-weight: 600;">
              <h6 style="font-size:14px; display:block; float:left; font-variant:small-caps">
                Invertieren
              </h6>
            </a>
          </v-col>
        </v-row>
        <v-row style="margin:0px 0px 0px 0px">
          <v-checkbox v-for="x in paginatedItems" :key="x.item" hide-details="true" v-model="x.checked"
            style="width: 100%;" density="compact">
            <template v-slot:label>
              <span style="margin-left: 5px; font-size: 0.8rem; font-weight: 300; text-align: left;">{{ x.item }}</span>
            </template>
          </v-checkbox>
          <v-pagination v-if="localItems.length > 10" v-model="currentPage" density="compact"
            style="margin-left: auto; margin-right: auto;" :length="Math.ceil(localItems.length / itemsPerPage)"
            :total-visible="4"></v-pagination>
        </v-row>
      </div>
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
    const query = this.search.$state.localSearch.query[this.rkey] || [];

    const querySet = new Set(Array.isArray(query) ? query : [query]);

    this.syncLock = true;
    this.localItems = Array.from(items).map(i => ({
      item: i,
      checked: querySet.has(i)
    })).sort((a, b) => {
      if (a.item < b.item) return -1;
      if (a.item > b.item) return 1;
      return 0;
    });
    this.syncLock = false;
  },

  data() {
    return {
      search: {},
      localItems: [],
      currentPage: 1,
      itemsPerPage: 10,
      syncLock: false
    }
  },

  methods: {
    selectAll() {
      this.localItems.forEach(x => x.checked = true);
    },
    selectNone() {
      this.localItems.forEach(x => x.checked = false);
    },
    selectInvert() {
      this.localItems.forEach(x => x.checked = !x.checked);
    }
  },

  watch: {
    localItems: {
      handler: function (val) {
        if (!this.syncLock)
          try {
            this.search.$state.localSearch.query[this.rkey] = val.filter(x => x.checked).map(x => x.item);
            this.$emit("changed");
          } catch {
            // ignore
          }
      },
      deep: true
    }
  },

  computed: {
    allSameValue() {
      return this.localItems.length && (this.localItems.every(x => x.checked) || this.localItems.every(x => !x.checked));
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