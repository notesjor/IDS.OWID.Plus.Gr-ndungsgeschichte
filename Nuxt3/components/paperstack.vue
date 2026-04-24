<template>
  <v-card @click="$emit('callback', source.id)" style="max-width: 740px; min-width: 740px; margin-bottom: 10px;" elevation="0">
    <v-card-text>
      <v-col cols="1" style="margin:-15px 0px 0px -15px">
        <div style="margin-left: auto; margin-right: auto; display: inline-block;">
          <v-icon v-if="locked" size="40px" style="margin: 0px 0px 0px 0px;">mdi-archive-outline</v-icon>
          <img v-else :src="`/thumbnail/${source.id}.jpg`" alt="Aktenstapel - Symbolbild"
            style="max-width: 100%; padding: 3px;" loading="lazy" />
        </div>
      </v-col>
      <v-col cols="11">
        <div style="margin:-75px 0px 0px 60px">
          <v-row>
            <div style="width: 100%; font-weight: 400; font-size: 1.1rem;">
              {{ source.title }}
            </div>
          </v-row>
          <v-row>            
            <personCounter :source="source" />
          </v-row>
        </div>
      </v-col>
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
    locked: {
      type: Boolean,
      default: false
    }
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
