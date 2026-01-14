<template>
  {{ source }}
</template>

<script>
export default {
  name: 'companyListItem',
  props: {
    source: {
      type: Object,
      default: {}
    },
  }
}
</script>