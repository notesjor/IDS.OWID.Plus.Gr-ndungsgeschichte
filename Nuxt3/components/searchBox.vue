<template>
  <v-expansion-panel>
    <v-expansion-panel-title>
      <gradient :style="styleGradient" :color1="color1" v-if="allSameValue"
        style="min-width: 35px; min-height: 50px; margin: -20px 5px -20px -15px" />
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
          <v-checkbox v-for="x in items" :key="x.item" hide-details="true" v-model="x.checked" style="width: 100%;"
            density="compact">
            <template v-slot:label>
              <span v-html="x.item" style="margin-left: 5px;"></span>
            </template>
          </v-checkbox>
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
    items: {
      type: [Array, Set],
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
    const source = Array.from(this.items)

    this.localItems = source.map(i => ({
      item: i,
      checked: false
    }))
  },

  data() {
    return {
      localItems: []
    }
  },

  methods: {
    selectAll() {
      this.items.forEach(x => x.checked = true);
    },
    selectNone() {
      this.items.forEach(x => x.checked = false);
    },
    selectInvert() {
      this.items.forEach(x => x.checked = !x.checked);
    }
  },

  watch: {
    items: {
      handler: function (val) {
        try {
          var values = val.filter(x => x.checked).map(x => x.item);
          this.$emit("change", { rkey: this.rkey, values: values });
        } catch {
          // ignore
        }
      },
      deep: true
    }
  },

  computed: {
    allSameValue() {
      return this.localItems.length && this.localItems.every(x => x.checked);
    },
    styleGradient() {
      return this.allSameValue ? "margin: -17px 5px -17px -25px; max-width:10px" : "margin: -17px 10px -17px -25px; max-width: 50px "
    },
    styleIcon() {
      return `margin: 0px 15px 0px -10px; color: ${this.$props.color1};`;
    }
  }
}
</script>