<template>
  <v-card style="max-width: 315px; min-width: 315px; min-height: 475px;" @click="$emit('callback', source.id)">
    <v-card-text>
      <v-row>
        <div style="margin-right: auto;">
          <v-tooltip text="Jahr (Erstellung)" location="top">
            <template v-slot:activator="{ props }">
              <v-chip v-bind="props" prepend-icon="mdi-calendar-range" variant="text">
                {{ source.date.substring(0, 4) }}
              </v-chip>
            </template>
          </v-tooltip>
        </div>
        <div style="margin-left: auto;">
          <v-tooltip text="Sigle (Regal/Ordner/Dokument-Nr.)" location="top">
            <template v-slot:activator="{ props }">
              <v-chip v-bind="props" prepend-icon="mdi-file-cabinet" variant="text">
                {{ source.id.replace("_", "/").replace("_", "/") }}
              </v-chip>
            </template>
          </v-tooltip>
        </div>
      </v-row>
      <v-row style="margin-top:13px">
        <div style="margin-right: auto;">
          <v-tooltip text="Text-Typ" location="top">
            <template v-slot:activator="{ props }">
              <v-chip v-bind="props" prepend-icon="mdi-shape-outline" variant="text">
                {{ source.text_type }}
              </v-chip>
            </template>
          </v-tooltip>
        </div>
        <div style="margin-left: auto;">
          <v-tooltip text="Anzahl-Seiten" location="top">
            <template v-slot:activator="{ props }">
              <v-chip v-bind="props" prepend-icon="mdi-book-open-page-variant-outline" variant="text">
                {{ source.pages }}
              </v-chip>
            </template>
          </v-tooltip>
        </div>
      </v-row>
      <v-row>
        <div style="margin-left: auto; margin-right: auto;">
          <img :src="`thumbnail/${source.id}.jpg`" alt="Aktenstapel - Symbolbild" v-if="source.has_fulltext"
            style="min-height: 200px; max-height: 200px; max-width: 280px; padding: 10px;" loading="lazy" />
          <v-icon v-else style="min-height: 200px; font-size: 128px;">mdi-note-text-outline</v-icon>
        </div>
      </v-row>
      <v-row>
        <div
          style="width: 100%; text-align: center; font-weight: 300; font-size: 0.9rem; margin-top: 0.5rem; margin-bottom: 0.5rem;">
          {{ source.title }}
        </div>
      </v-row>
      <div style="position: absolute; bottom: 5px">
        <div style="margin-right: auto; margin-left:auto;">
          <personCounter :source="source" />
        </div>
      </div>

    </v-card-text>
  </v-card>
</template>

<script>
export default {
  name: 'StackedPaperImage',
  props: {
    source: {
      type: Object,
      default: {}
    },
  },
  data() {
    return {
      dialog: false
    }
  },
  mounted() {
  }
}
</script>
