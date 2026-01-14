<template>
  <personListItem v-for="x in data" :key="x" :source="x" />
</template>

<script>
export default {
  name: 'personList',
  props: {
    data: {
      type: Array,
      default: []
    },
  }
}
</script>
