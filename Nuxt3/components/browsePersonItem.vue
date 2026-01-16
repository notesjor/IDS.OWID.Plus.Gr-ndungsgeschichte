<template>
  <div style="padding-bottom: 5px;">
    <div>
      <span>{{ k }} (<i>{{ person.year_birth }}-{{ person.year_death }}</i>)</span>:&nbsp;
      <span @click="search()" class="btn"><v-icon>mdi-text-box-search-outline</v-icon>
        <span>{{ v }}</span></span>
    </div>
    <div style="font-weight: 200;">
      {{ person.info }}
      <span style="margin:-5px 0px 0px -5px">
        <a :href="person.wiki_url"
          v-if="person.wiki_url != undefined && person.wiki_url != null && person.wiki_url.length > 0"
          style="margin:0px 0px 0px 5px" target="_blank">
          <v-tooltip text="Personen-Eintrag in Wikidata öffnen" location="top">
            <template v-slot:activator="{ props }">
              <img v-bind="props" src="/logos/wiki.svg" alt="GND-Link"
                style="max-height: 20px; min-height: 20px; display: inline-block;" />
            </template>
          </v-tooltip>
        </a>
        <a :href="person.gnd_url"
          v-if="person.gnd_url != undefined && person.gnd_url != null && person.gnd_url.length > 0"
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
      person: {}
    }
  },
  mounted() {
    var search = useSearchStore();
    search.initialize();
    this.person = search.getPersonById(this.k);
    console.log(this.person)
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