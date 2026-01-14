<template>
  {{ source }}
</template>

<script>
export default {
  name: 'documentListItem',
  props: {
    source: {
      type: Object,
      default: {}
    },
  }
}
</script>