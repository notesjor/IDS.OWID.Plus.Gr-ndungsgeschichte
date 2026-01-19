<template>
  <personListItem v-for="x in data" :key="x" :source="x" :rkey="rkey"/>
</template>

<script>
export default {
  name: 'personList',
  props: {
    data: {
      type: Array,
      default: []
    },
    rkey: {
      type: String
    }
  }
}
</script>
