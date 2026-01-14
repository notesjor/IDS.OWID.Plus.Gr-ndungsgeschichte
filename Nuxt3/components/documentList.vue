<template>
  <documentListItem v-for="x in data" :key="x" :source="x" />
</template>

<script>
export default {
  name: 'documentList',
  props: {
    data: {
      type: Array,
      default: []
    },
  }
}
</script>
