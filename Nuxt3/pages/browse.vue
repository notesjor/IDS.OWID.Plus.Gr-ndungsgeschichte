<script setup>
useHead({
  title: "IDS‑Gründungsgeschichte: Verzeichnis"
})
</script>

<template>
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto;">
      <h1 style="margin-top:1rem">Verzeichnis</h1>
    </v-col>
  </v-row>
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 2rem; margin-top: -20px;">
      Kurzbeschreibung: Lorem ipsum dolor sit amet consectetur, adipisicing elit. Expedita reiciendis beatae velit quod
      tempora magni corrupti unde alias, excepturi nulla maxime atque, cum neque quibusdam minus amet eveniet
      repellendus deleniti?
    </v-col>
  </v-row>
  <v-row>
    <v-col style="margin:-15px 0px 0px -15px">
      <v-row style="margin:0px 50px 0px 50px">
        <v-col cols="4">
          <div class="bg-white rounded shadow-md border border-gray-100 min-h-[100px]">
            <div style="position: sticky; top: 63px; z-index: 10; background-color: white;">
              <div style="margin:10px 0px 10px 0px;">
                <v-icon size="32" class="mt-4" style="margin: 0px 10px 0px 20px; float: left;">mdi-domain</v-icon>
                <div style="margin-left: 10px; font-weight: 700; font-size: 1.1rem;">Institutionen</div>
                <div
                  style="margin-left: 10px; color: gray; font-weight: 200; font-size:0.9rem; margin:-3px 0px 0px -3px">
                  Referenzierte Institution im Findebuch</div>
              </div>
              <div class="bg-surface-light p-4" style="margin: 0px 0px 10px 0px;">
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
            </div>
            <div style="padding: 0px 20px 10px 20px;">
              <div v-for="(v, k) in counter_company_filtred" :key="k" style="text-align: left; margin-bottom: 10px;">
                <browseCompanyItem :v="v" :k="k" />
              </div>
            </div>
          </div>
        </v-col>
        <v-col cols="4">
          <div class="bg-white rounded shadow-md border border-gray-100 min-h-[100px]">
            <div style="position: sticky; top: 63px; z-index: 10; background-color: white;">
              <div style="margin:10px 0px 10px 0px;">
                <v-icon size="32" class="mt-4" style="margin: 0px 10px 0px 20px; float: left;">mdi-account</v-icon>
                <div style="margin-left: 10px; font-weight: 700; font-size: 1.1rem;">Personen</div>
                <div
                  style="margin-left: 10px; color: gray; font-weight: 200; font-size:0.9rem; margin:-3px 0px 0px -3px">
                  Beteiligte Personen im Schriftverkehr</div>
              </div>
              <div class="bg-surface-light p-4" style="margin: 0px 0px 10px 0px;">
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
            </div>
            <div style="padding: 0px 20px 10px 20px;">
              <div v-for="(v, k) in counter_persons_filtred" :key="k" style="text-align: left; margin-bottom: 10px;">
                <browsePersonItem :v="v" :k="k" />
              </div>
            </div>
          </div>
        </v-col>
        <v-col cols="4">
          <div class="bg-white rounded shadow-md border border-gray-100 min-h-[100px]">
            <div style="position: sticky; top: 63px; z-index: 10; background-color: white;">
              <div style="margin:10px 0px 10px 0px;">
                <v-icon size="32" class="mt-4" style="margin: 0px 10px 0px 20px; float: left;">mdi-tag-text</v-icon>
                <div style="margin-left: 10px; font-weight: 700; font-size: 1.1rem;">Schlagworte</div>
                <div
                  style="margin-left: 10px; color: gray; font-weight: 200; font-size:0.9rem; margin:-3px 0px 0px -3px">
                  zu Themen und Inhalten</div>
              </div>
              <div class="bg-surface-light p-4" style="margin: 0px 0px 10px 0px;">
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
            </div>
            <div style="padding: 0px 20px 10px 20px;">
              <div v-for="(v, k) in counter_keyword_filtred" :key="k" style="text-align: left; margin-bottom: 10px;">
                <browseKeywordItem :v="v" :k="k" />
              </div>
            </div>
          </div>
        </v-col>
      </v-row>
    </v-col>
  </v-row>
  <scrollUp />
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
      void Object.keys(this.counter_company).length;
      return { ...this.filter(this.counter_company, this.company_filter) };
    },
    counter_persons_filtred() {
      void Object.keys(this.counter_persons).length;
      return { ...this.filter(this.counter_persons, this.persons_filter) };
    },
    counter_keyword_filtred() {
      void Object.keys(this.counter_keyword).length;
      return { ...this.filter(this.counter_keyword, this.keyword_filter) };
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