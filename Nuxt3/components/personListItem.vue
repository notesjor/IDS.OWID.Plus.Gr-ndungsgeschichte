<template>
  <div style="margin:20px 0px">
    <v-row>
      <span>{{ data.id }} (<i>{{ data.year_birth }}-{{ data.year_death }}</i>)</span>
      <a :href="data.wiki_url" v-if="data.wiki_url != undefined && data.wiki_url != null && data.wiki_url.length > 0"
        style="margin:0px 0px 0px 5px" target="_blank">
        <v-tooltip text="Personen-Eintrag in Wikidata öffnen" location="top">
          <template v-slot:activator="{ props }">
            <img v-bind="props" src="/logos/wiki.svg" alt="GND-Link" style="max-height: 24px; min-height: 24px; display: inline-block;" />
          </template>
        </v-tooltip>
      </a>
      <a :href="data.gnd_url" v-if="data.gnd_url != undefined && data.gnd_url != null && data.gnd_url.length > 0"
        style="margin:0px 0px 0px 5px" target="_blank">
        <v-tooltip text="Personen-Eintrag in der GND öffnen" location="top">
          <template v-slot:activator="{ props }">
            <img v-bind="props" src="/logos/gnd.svg" alt="GND-Link" style="max-height: 24px; min-height: 24px; display: inline-block;" />
          </template>
        </v-tooltip>
      </a>
      <span>:</span>
      <personCounter :source="data" mode="author" style="margin:-3px 0px 0px 0px" />
    </v-row>
    <v-row>
      <span style="margin:-5px 0px 0px 20px; font-size:0.9rem; font-weight: 300;">{{ data.info }}</span>
    </v-row>
  </div>
</template>

<script>
import esQuick from "../api/esQuick.js";

export default {
  name: 'personListItem',
  props: {
    source: {
      type: String,
      default: ""
    },
  },
  data() {
    return {
      esQuick: null,
      data: {}
    }
  },
  mounted() {
    this.esQuick = new esQuick();
    this.esQuick.initialize("http://localhost:9200", "gruendungsgeschichte_person");
    this.loadItem(this.source);
  },

  methods: {
    loadItem(id) {
      var self = this;
      this.esQuick.getDocumentById(id, (json) => {
        self.data = json._source;
      });
    }
  }
}
</script>