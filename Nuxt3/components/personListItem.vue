<template>
  <div style="margin:20px 0px">
    <v-row>
      <span class="btn" @click="applyFilter(rkey, data.id)">
        {{ data.id }} (<i>{{ data.year_birth }}-{{ data.year_death }}</i>)
      </span>
      <a :href="data.wiki_url" v-if="data.wiki_url != undefined && data.wiki_url != null && data.wiki_url.length > 0"
        style="margin:0px 0px 0px 5px" target="_blank">
        <v-tooltip text="Personen-Eintrag in Wikidata öffnen" location="top">
          <template v-slot:activator="{ props }">
            <img v-bind="props" src="/logos/wiki.svg" alt="GND-Link"
              style="max-height: 24px; min-height: 24px; display: inline-block;" />
          </template>
        </v-tooltip>
      </a>
      <a :href="data.gnd_url" v-if="data.gnd_url != undefined && data.gnd_url != null && data.gnd_url.length > 0"
        style="margin:0px 0px 0px 5px" target="_blank">
        <v-tooltip text="Personen-Eintrag in der GND öffnen" location="top">
          <template v-slot:activator="{ props }">
            <img v-bind="props" src="/logos/gnd.svg" alt="GND-Link"
              style="max-height: 24px; min-height: 24px; display: inline-block;" />
          </template>
        </v-tooltip>
      </a>
      <span>:</span>
      <personCounterSmall :source="data" mode="author" style="margin:-3px 0px 0px 0px" />
    </v-row>
    <v-row>
      <span style="margin:0px 0px 10px 20px; font-size:0.9rem; font-weight: 300;">{{ data.info }}</span>
    </v-row>
  </div>
</template>

<script>
import { useSearchStore } from '~/stores/search';

export default {
  name: 'personListItem',
  props: {
    source: {
      type: String,
      default: ""
    },
    rkey: {
      type: String
    }
  },
  data() {
    return {
      search: null,
      data: {}
    }
  },
  mounted() {
    this.search = useSearchStore();
    this.search.initialize();
    this.data = this.search.getPersonById(this.source);
  },
  methods:
  {
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
  text-align: left;
}

.btn:hover {
  background-color: rgb(229, 231, 235);
}
</style>