<template>
  <div style="padding-bottom: 5px;">
    <div>
      <span>{{ k }} (<i>{{ info.year_birth }}-{{ info.year_death }}</i>)</span>:&nbsp;
      <v-tooltip text="Dokumente mit Bezug zu dieser Person" location="top">
        <template v-slot:activator="{ props }">
          <span v-bind="props" @click="search()" class="btn"><v-icon>mdi-text-box-search-outline</v-icon>
            <span>{{ v }}</span>
          </span>
        </template>
      </v-tooltip>
    </div>
    <div style="font-weight: 200;">
      {{ info.info }}
      <span style="margin:-5px 0px 0px -5px">
        <a :href="info.wiki_url" v-if="info.wiki_url != undefined && info.wiki_url != null && info.wiki_url.length > 0"
          style="margin:0px 0px 0px 5px" target="_blank">
          <v-tooltip text="Personen-Eintrag in Wikidata öffnen" location="top">
            <template v-slot:activator="{ props }">
              <img v-bind="props" src="/logos/wiki.svg" alt="GND-Link"
                style="max-height: 20px; min-height: 20px; display: inline-block;" />
            </template>
          </v-tooltip>
        </a>
        <a :href="info.gnd_url" v-if="info.gnd_url != undefined && info.gnd_url != null && info.gnd_url.length > 0"
          style="margin:0px 0px 0px 5px" target="_blank">
          <v-tooltip text="Personen-Eintrag in der GND öffnen" location="top">
            <template v-slot:activator="{ props }">
              <img v-bind="props" src="/logos/gnd.svg" alt="GND-Link"
                style="max-height: 20px; min-height: 20px; display: inline-block;" />
            </template>
          </v-tooltip>
        </a>
      </span>
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
    if (search.getPersonById(this.k) == undefined)
      this.info = {
        year_birth: "",
        year_death: "",
        info: ""
      }
    else
      this.info = search.getPersonById(this.k);

    if (this.info.info == "")
      this.info.info = "Zur Person konnten keine Informationen ermittelt werden.";
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