<template>
  <span>{{ k }}</span>:&nbsp;
  <span @click="search()" class="btn"><v-icon>mdi-text-box-search-outline</v-icon>
  <span>{{ v }}</span></span>
</template>

<script>
export default {
  props:{
    k:{
      type: String,
    },
    v: {
      type: Number
    }
  },

  methods: {
    search(){

    }
  },

  computed: {
  }
}
</script>

<style scoped>
.btn {
  border-bottom: 1px dotted #000;
  background-color: white;
  cursor: pointer;
  font-weight: 200;
  text-align: left;
}

.btn:hover {
  background-color: rgb(229, 231, 235);
}
</style>