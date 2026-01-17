<template>
  <div style="padding-bottom: 5px;">
    <div>
      <span>{{ source }}
        <span v-if="info.city != undefined && info.city != null && info.city.length > 0">[<i>{{ info.city }}</i>]</span>
        &nbsp;
      </span>
    </div>
  </div>
</template>

<script>
import { useSearchStore } from '~/stores/search';

export default {
  name: 'companyListItem',
  props: {
    source: {
      type: String,
      default: ''
    },
  },
  data() {
    return {
      info: {}
    }
  },
  mounted() {
    var search = useSearchStore();
    search.initialize();
    if (search.getCompanyById(this.source) == undefined)
      this.info = {
        city: ""
      }
    else
      this.info = search.getCompanyById(this.source);
  },
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