<template>
  <div style="padding-bottom: 5px;">
    <div>
      <span>{{ k }}
        <span v-if="info.city != undefined && info.city != null && info.city.length > 0">[<i>{{ info.city }}</i>]</span>
        &nbsp;
      </span>
      <v-tooltip text="Dokumente mit Bezug zu dieser Institution" location="top">
        <template v-slot:activator="{ props }">
          <span v-bind="props" @click="search()" class="btn"><v-icon>mdi-text-box-search-outline</v-icon>
            <span>{{ v }}</span>
          </span>
        </template>
      </v-tooltip>
    </div>
  </div>
</template>

<script>
import { useSearchStore } from '~/stores/search';

export default {
  props: {
    k: {
      type: String,
    },
    v: {
      type: Number
    }
  },
  data() {
    return {
      info: {}
    }
  },
  mounted() {
    var search = useSearchStore();
    search.initialize();
    if (search.getCompanyById(this.k) == undefined)
      this.info = {
        city: ""
      }
    else
      this.info = search.getCompanyById(this.k);
  },

  methods: {
    search() {

    }
  },
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