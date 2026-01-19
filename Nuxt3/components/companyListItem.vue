<template>
  <div style="padding-bottom: 5px;">
    <div>
      <span style="text-align: left;">
        <span class="btn" @click="applyFilter('referenced_companies', source)">{{ source }}</span>
        &nbsp;
        <span v-if="info.city != undefined && info.city != null && info.city.length > 0">[<i>{{ info.city }}</i>]</span>
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
  methods:
  {
    applyFilter(key, value) {
      const params = new URLSearchParams(window.location.search);
      console.log(window.location.pathname);
      params.set(key, value);
      const newUrl = `/plus/idsgg/search?${params.toString()}`;
      window.history.pushState({}, '', newUrl);
      window.location.reload();
    }
  }
}
</script>

<style scoped>
.btn {
  border-bottom: 1px dotted #000;
  background-color: white;
  cursor: pointer;
  text-align: left;
}

.btn:hover {
  background-color: rgb(229, 231, 235);
}
</style>