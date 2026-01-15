<template>
  <projectHeader />
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 2rem;">
      Kurzbeschreibung: Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti?
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="2" style="padding: 0px 30px 0px 20px;">
    </v-col>
    <v-col cols="9" style="margin:-15px 0px 0px -15px">
      <v-row>
        <v-col cols="4">
          <v-card class="mx-auto" prepend-icon="mdi-domain" subtitle="Referenzierte Institution im Findebuch"
            width="400">
            <template v-slot:title>
              <span class="font-weight-black">Institutionen</span>
            </template>

            <v-card-text class="bg-surface-light pt-4">
              <div v-for="(v, k) in counter_company" :key="key" style="text-align: left; margin-bottom: 10px;">
                <span>{{ k }}</span>&nbsp;<span>({{ v }})</span>
              </div>
            </v-card-text>
          </v-card>
        </v-col>
        <v-col cols="4">
          <v-card class="mx-auto" prepend-icon="mdi-account" subtitle="Beteiligte Personen im Schriftverkehr"
            width="400">
            <template v-slot:title>
              <span class="font-weight-black">Personen</span>
            </template>

            <v-card-text class="bg-surface-light pt-4">
              <div v-for="(v, k) in counter_persons" :key="key" style="text-align: left; margin-bottom: 10px;">
                <span>{{ k }}</span>&nbsp;<span>({{ v }})</span>
              </div>
            </v-card-text>
          </v-card>
        </v-col>
        <v-col cols="4">
          <v-card class="mx-auto" prepend-icon="mdi-tag-text" subtitle="zu Themen und Inhalten" width="400">
            <template v-slot:title>
              <span class="font-weight-black">Schlagworte</span>
            </template>

            <v-card-text class="bg-surface-light pt-4">
              <div v-for="(v, k) in counter_keyword" :key="key" style="text-align: left; margin-bottom: 10px;">
                <span>{{ k }}</span>&nbsp;<span>({{ v }})</span>
              </div>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-col>
    <v-col></v-col>
  </v-row>
</template>

<script>
import { useSearchStore } from '~/stores/search';

export default {
  data() {
    return {
      counter_company: {},
      counter_persons: {},
      counter_keyword: {},
    }
  },
  mounted() {
    var search = useSearchStore();
    search.initialize();

    this.counter_company = this.calcFrequency(search, "referenced_companies", {});
    this.counter_company = this.sortByFrequency(this.counter_company);

    this.counter_persons = this.calcFrequency(search, "persons_author", {});
    this.counter_persons = this.calcFrequency(search, "persons_recipient", this.counter_persons);
    this.counter_persons = this.calcFrequency(search, "persons_recipient", this.counter_persons);    
    this.counter_persons = this.sortByFrequency(this.counter_persons);

    this.counter_keyword = this.calcFrequency(search, "content", {});
    this.counter_keyword = this.sortByFrequency(this.counter_keyword);
  },

  methods: {
    calcFrequency(search, propertyName, orig) {
      search.localSearch.data.forEach(item => {
        if (Array.isArray(item[propertyName])) {
          item[propertyName].forEach(str => {
            orig[str] = (orig[str] || 0) + 1;
          });
        }
      });
      return orig;
    },
    sortByKey(orig) {
      return Object.keys(orig)
        .sort()
        .reduce((sorted, key) => {
          sorted[key] = orig[key];
          return sorted;
        }, {});
    }, sortByFrequency(orig) {
      return Object.entries(orig)
        .sort(([, valueA], [, valueB]) => valueB - valueA)
        .reduce((sorted, [key, value]) => {
          sorted[key] = value;
          return sorted;
        }, {});
    }
  },

}
</script>

<style lang="scss" scoped></style>