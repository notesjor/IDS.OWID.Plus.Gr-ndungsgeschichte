<template>
  <companyListItem v-for="x in data" :key="x" :source="x" />
</template>

<script>
export default {
  name: 'companyList',
  props: {
    data: {
      type: Array,
      default: []
    },
  }
}
</script>
