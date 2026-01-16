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

            <v-card-text class="pt-4">
              <div class="bg-surface-light p-4" style="margin: -20px -20px 10px -20px;">
                <div>Sortierung:&nbsp;
                  <span @click="sort('company', 'alpha')" class="btn bg-surface-light"
                    :style="sort_co_alpha">Alphabetisch</span>
                  &nbsp;/&nbsp;
                  <span @click="sort('company', 'frequ')" class="btn bg-surface-light"
                    :style="sort_co_frequ">Häufigkeit</span>
                </div>
                <div>
                  <v-text-field v-model="company_filter" clearable label="Instituionen filtern..."
                    style="margin: 10px 0px -20px 0px"></v-text-field>
                </div>
              </div>
              <div v-for="(v, k) in counter_company_filtred" :key="k" style="text-align: left; margin-bottom: 10px;">
                <browseCompanyItem :v="v" :k="k"/>
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

            <v-card-text class="pt-4">
              <div class="bg-surface-light p-4" style="margin: -20px -20px 10px -20px;">
                <div>Sortierung:&nbsp;
                  <span @click="sort('persons', 'alpha')" class="btn bg-surface-light"
                    :style="sort_pe_alpha">Alphabetisch</span>
                  &nbsp;/&nbsp;
                  <span @click="sort('persons', 'frequ')" class="btn bg-surface-light"
                    :style="sort_pe_frequ">Häufigkeit</span>
                </div>
                <div>
                  <v-text-field v-model="persons_filter" clearable label="Personen filtern..."
                    style="margin: 10px 0px -20px 0px"></v-text-field>
                </div>
              </div>
              <div v-for="(v, k) in counter_persons_filtred" :key="k" style="text-align: left; margin-bottom: 10px;">
                <browsePersonItem :v="v" :k="k"/>
              </div>
            </v-card-text>
          </v-card>
        </v-col>
        <v-col cols="4">
          <v-card class="mx-auto" prepend-icon="mdi-tag-text" subtitle="zu Themen und Inhalten" width="400">
            <template v-slot:title>
              <span class="font-weight-black">Schlagworte</span>
            </template>

            <v-card-text class="pt-4">
              <div class="bg-surface-light p-4" style="margin: -20px -20px 10px -20px;">
                <div>Sortierung:&nbsp;
                  <span @click="sort('keyword', 'alpha')" class="btn bg-surface-light"
                    :style="sort_kw_alpha">Alphabetisch</span>
                  &nbsp;/&nbsp;
                  <span @click="sort('keyword', 'frequ')" class="btn bg-surface-light"
                    :style="sort_kw_frequ">Häufigkeit</span>
                </div>
                <div>
                  <v-text-field v-model="keyword_filter" clearable label="Schlagworte filtern..."
                    style="margin: 10px 0px -20px 0px"></v-text-field>
                </div>
              </div>
              <div v-for="(v, k) in counter_keyword_filtred" :key="k" style="text-align: left; margin-bottom: 10px;">
                <browseKeywordItem :v="v" :k="k"/>
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

      company_filter: "",
      persons_filter: "",
      keyword_filter: "",

      company_sort: "frequ",
      persons_sort: "frequ",
      keyword_sort: "frequ",
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
    },
    search() {

    },
    sort(prop, type) {
      this[prop + "_sort"] = type;
      if (type == "frequ")
        this[`counter_${prop}`] = this.sortByFrequency(this[`counter_${prop}`])
      else
        this[`counter_${prop}`] = this.sortByKey(this[`counter_${prop}`])
    },
    filter(input, query) {
      const res = {};
      for (const key in input) {
        if (key.includes(query)) {
          res[key] = input[key];
        }
      }
      return res;
    }
  },

  computed: {
    sort_co_alpha() {
      return this.company_sort == "alpha" ? "border-bottom: 1px solid #000;" : "";
    },
    sort_co_frequ() {
      return this.company_sort == "frequ" ? "border-bottom: 1px solid #000;" : "";
    },
    sort_pe_alpha() {
      return this.persons_sort == "alpha" ? "border-bottom: 1px solid #000;" : "";
    },
    sort_pe_frequ() {
      return this.persons_sort == "frequ" ? "border-bottom: 1px solid #000;" : "";
    },
    sort_kw_alpha() {
      return this.keyword_sort == "alpha" ? "border-bottom: 1px solid #000;" : "";
    },
    sort_kw_frequ() {
      return this.keyword_sort == "frequ" ? "border-bottom: 1px solid #000;" : "";
    },
    counter_company_filtred() {
      return this.filter(this.counter_company, this.company_filter);
    },
    counter_persons_filtred() {
      return this.filter(this.counter_persons, this.persons_filter);
    },
    counter_keyword_filtred() {
      return this.filter(this.counter_keyword, this.keyword_filter);
    },
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