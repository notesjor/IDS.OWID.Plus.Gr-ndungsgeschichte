<script setup>
useHead({
  title: "IDS‑Gründungsgeschichte: Findebuch"
})
</script>

<template>
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto;">
      <h1 style="margin-top:1rem">Findebuch</h1>
    </v-col>
  </v-row>
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 2rem; margin-top: -20px;">
      Das Findebuch umfasst alle Texte des IDS-Vorstandsarchivs der Jahre 1963–1970. Auf der linken Seite finden Sie
      verschiedene Facetten, über die Sie gezielt suchen können. Jeder gefundene Eintrag verweist auf wichtige Metriken
      wie Datum, Texttyp, Sigle oder Seitenanzahl. Unten in der Fußleiste jedes Eintrags sehen Sie außerdem, wie viele
      Personen oder Institutionen mit dem jeweiligen Text verknüpft sind.
      <br />
      Texte, die Bilder enthalten, sind als Volltext verfügbar. Die computergenerierten Bilder der Textstapel vermitteln
      zudem einen Eindruck davon, wie viele Seiten ein Dokument umfasst. Durch einen Klick auf einen Text erhalten Sie
      zusätzliche Detailinformationen und können auch über diese Detailinformationen gezielt suchen.
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="3" style="padding: 0px 30px 0px 20px; position: sticky; top: 80px; align-self: start;">
      <v-expansion-panels v-model="panels" multiple v-if="search != null">
        <searchBox @changed="reload" title="Regal-Nummer" rkey="archive_rack" />
        <searchBox @changed="reload" title="Ordner-Name" rkey="archive_folder_name" />
        <searchBox @changed="reload" title="Texttyp" rkey="text_type" />
        <searchBox @changed="reload" title="Autor*in(nen)" rkey="persons_author" />
        <searchBox @changed="reload" title="Empfänger*in(nen)" rkey="persons_recipient" />
        <searchBox @changed="reload" title="Referenzierte Personen" rkey="persons_referenced" />
        <searchBox @changed="reload" title="Referenzierte Institutionen" rkey="referenced_companies" />
        <searchBox @changed="reload" title="Schlagwörter (Inhalte/Themen)" rkey="content" />
        <searchBoxYearRange @changed="reload" title="Zeitraum" rkey="date" />
      </v-expansion-panels>
    </v-col>
    <v-col cols="9" style="margin:-15px 0px 0px -15px">

      <div style="display:flex; flex-wrap:wrap; gap:0.75rem; align-items:flex-start;">
        <div v-if="loading" style="position:absolute; inset:1; display:flex; align-items:center; 
          justify-content:center; background-color: rgba(255,255,255,0.85); z-index: 999;
          margin-left: auto; margin-right: auto;">
          <div class="d-flex flex-column align-center justify-center"
            style="padding:20px; background:white; border-radius:6px; box-shadow:0 2px 12px rgba(0,0,0,0.12);">
            <v-icon :size="60" class="mb-4">mdi-magnify</v-icon>
            <div>Daten werden geladen...</div>
          </div>
        </div>
        <div v-show="!loading" style="display:flex; flex-wrap:wrap; gap:0.75rem; align-items:flex-start;" id="results">
          <div v-for="(v, k) in esResults" :key="k">
            <paperstack :source="v" @callback="showItemDialog" />
          </div>
        </div>
      </div>
    </v-col>
  </v-row>

  <v-dialog max-width="800" v-model="dialog" style="margin:75px 0px 0px 0px; z-index:99999">
    <v-card>
      <v-card-text style="max-width: 100ch; padding:40px">
        <v-row style="font-size:1.25rem; font-weight: 500;">Dokument: {{ dialogData.archive_rack }}/{{
          dialogData.archive_folder }}/{{ dialogData.archive_document }}</v-row>
        <v-row style="font-weight: 300; font-size: 0.85rem; margin-bottom: 20px;">
          <p>
            <span style="font-weight: 500;">Hinweis:&nbsp;</span>
            <span style="text-align: left;">Unterstrichene Eigenschaften, Schlagwörter und verknüpfte Entitäten können
              angeklickt
              werden, um ähnliche Dokumente zu finden.</span>
          </p>
        </v-row>
        <v-row>
          <div style="display: block; width:100%">Titel (Ordner):</div>
          <div @click="applyFilter('archive_folder_name', dialogData.archive_folder_name)" class="btn">{{
            dialogData.archive_folder_name }}</div>
        </v-row>
        <v-row>
          <div style="display: block; width:100%">Titel (Dokument):</div>
          <div @click="applyFilter('archive_folder_name', dialogData.title)"
            style="font-weight: 200; margin: 0px 10px 15px 20px; text-align: left;">{{
              dialogData.title }}</div>
        </v-row>
        <v-row>
          <span>
            Texttyp:&nbsp;
            <span @click="applyFilter('text_type', dialogData.text_type)" class="btn" style="margin-left: 0px;">{{
              dialogData.text_type }}</span>
          </span>
        </v-row>
        <v-row>
          <span>
            Datum:&nbsp;
            <span style="font-weight: 200;">
              <span @click="applyFilter('date', dialogData.date.substring(0, 4))" class="btn" style="margin: 0px;">{{
                dialogData.date.substring(0, 4) }}</span>
              <span v-if="dialogData.date.length > 4">{{ dialogData.date.substring(4) }}</span>
            </span>
            /&nbsp;Anzahl Seiten:&nbsp;
            <span @click="applyFilter('pages', dialogData.pages)" class="btn" style="margin: 0px;">{{ dialogData.pages
            }}</span>
          </span>
        </v-row>
        <v-row>
          <v-col cols="3">
            <img :src="`/plus/idsgg/thumbnail/${dialogData.id}.jpg`" alt="Bild auf oberstem Blatt"
              style="max-height: 300px; max-width: 480px; margin-left:auto; margin-right: auto;" />
            <v-btn prepend-icon="mdi-download-circle-outline" style="margin:5px 0px" v-if="dialogData.has_fulltext"
              :href="`/plus/idsgg/fulltext/${dialogData.id}.pdf`">
              Download</v-btn>
          </v-col>
          <v-col cols="1"></v-col>
          <v-col cols="8">
            Schlagwörter (Inhalt):<br />
            <div>
              <v-chip class="btnChip" v-for="x in dialogData.content" :key="x" style="margin:2px">{{ x }}</v-chip>
            </div>
          </v-col>
        </v-row>
        <v-row style="margin-top:30px">
          Verknüpfte Entitäten (Visualisierung):
          <div ref="cy" style="width:100%; height:420px; border:1px solid #eee; border-radius:6px;"></div>
        </v-row>
        <v-row style="margin-top:30px">
          Verknüpfte Entitäten (Liste mit Zusatzangaben):
          <v-expansion-panels variant="accordion">
            <v-expansion-panel v-if="dialogData.persons_author?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-email-fast-outline</v-icon>
                <span style="margin:0px 5px">Verfasser*innen</span>
                <span>(Anzahl: {{ dialogData.persons_author?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <personList :data="dialogData.persons_author" rkey="persons_author" />
              </v-expansion-panel-text>
            </v-expansion-panel>
            <v-expansion-panel v-if="dialogData.persons_recipient?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-email-newsletter</v-icon>
                <span style="margin:0px 5px">Adressat*innen</span>
                <span>(Anzahl: {{ dialogData.persons_recipient?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <personList :data="dialogData.persons_recipient" rkey="persons_recipient" />
              </v-expansion-panel-text>
            </v-expansion-panel>
            <v-expansion-panel v-if="dialogData.persons_referenced?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-comment-account-outline</v-icon>
                <span style="margin:0px 5px">Sonstige refernzierte Personen</span>
                <span>(Anzahl: {{ dialogData.persons_referenced?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <personList :data="dialogData.persons_referenced" rkey="persons_referenced" />
              </v-expansion-panel-text>
            </v-expansion-panel>
            <v-expansion-panel v-if="dialogData.referenced_companies?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-domain</v-icon>
                <span style="margin:0px 5px">Referenzierte Institutionen</span>
                <span>(Anzahl: {{ dialogData.referenced_companies?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <companyList :data="dialogData.referenced_companies" />
              </v-expansion-panel-text>
            </v-expansion-panel>
            <v-expansion-panel v-if="dialogData.referenced_documents?.length > 0">
              <v-expansion-panel-title>
                <v-icon>mdi-link-box-variant-outline</v-icon>
                <span style="margin:0px 5px">Referenzierte Dokumente</span>
                <span>(Anzahl: {{ dialogData.referenced_documents?.length }})</span>
              </v-expansion-panel-title>
              <v-expansion-panel-text>
                <documentList :data="dialogData.referenced_documents" />
              </v-expansion-panel-text>
            </v-expansion-panel>
          </v-expansion-panels>
        </v-row>        
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import { useSearchStore } from '~/stores/search';

import cytoscape from 'cytoscape';
import coseBilkent from 'cytoscape-cose-bilkent';
import {
  mdiMapMarkerRadiusOutline,
  mdiFileSign,
  mdiEmailNewsletter,
  mdiCommentAccountOutline,
  mdiDomain,
  mdiLinkBoxVariantOutline
} from '@mdi/js';

cytoscape.use(coseBilkent);

export default {
  data() {
    return {
      search: null,
      esResults: [],

      dialog: false,
      dialogData: {},
      panels: [],
      loading: false,

      cyInstance: null
    }
  },
  mounted() {
    this.appName = this.$config.public.appName;
    this.appDescription = this.$config.public.appDescription;

    this.search = useSearchStore();
    this.search.initialize();

    // Parse URL parameters
    const params = new URLSearchParams(window.location.search);
    const query = {};

    params.forEach((value, key) => {
      if (key === 'id') {
        this.showItemDialog(value);
      } else if (key === 'date') {
        const year = parseInt(value);
        query[key] = { from: year, to: year };
      } else {
        if (query[key]) {
          if (Array.isArray(query[key])) {
            query[key].push(value);
          } else {
            query[key] = [query[key], value];
          }
        } else {
          query[key] = value;
        }
      }
    });

    Object.keys(query).forEach((key) => {
      this.search.localSearch.query[key] = query[key];
    });

    this.reload();
  },

  methods: {
    reload() {
      this.loading = true;
      setTimeout(() => {
        this.esResults = this.search.getCurrentData();
        this.loading = false;
      }, 1);
    },
    showItemDialog(id) {
      this.dialogData = this.search.getById(id);
      this.dialog = true;
    },
    // Hilfsfunktion: SVG aus mdi-Pfad erzeugen und als data-uri zurückgeben
    mdiToDataUri(path, size = 48, color = '#222') {
      const svg = `<svg xmlns="http://www.w3.org/2000/svg" width="${size}" height="${size}" viewBox="0 0 24 24"><path fill="${encodeURIComponent(color)}" d="${path}"/></svg>`;
      return `data:image/svg+xml;charset=utf8,${encodeURIComponent(svg)}`;
    },

    // Initialisiert / (re)drawt den Cytoscape-Graphen für das aktuelle dialogData
    initGraph() {
      if (!this.dialogData || !this.$refs.cy) return;

      // zerstöre alten Graphen
      if (this.cyInstance) {
        this.cyInstance.destroy();
        this.cyInstance = null;
      }

      const elements = [];
      const centerId = `doc-${this.dialogData.id}`;

      // Zentrum: aktuelles Dokument
      elements.push({
        data: {
          id: centerId,
          label: `${this.dialogData.archive_rack}/${this.dialogData.archive_folder}/${this.dialogData.archive_document}`,
          type: 'document_center',
          icon: this.mdiToDataUri(mdiMapMarkerRadiusOutline, 56, '#0f172a')
        }
      });

      // Personen: authors / recipients / referenced (icon abhängig von Rolle)
      const addPersonNodes = (arr, roleKey, iconPath) => {
        if (!Array.isArray(arr)) return;
        arr.forEach((p, idx) => {
          // p kann ein String (Name) oder Objekt sein; handle beide Fälle
          const name = typeof p === 'string' ? p : p.name || p.label || JSON.stringify(p);
          const pid = `person-${roleKey}-${idx}-${name}`.replace(/\s+/g, '_');
          elements.push({
            data: {
              id: pid,
              label: name,
              type: 'person',
              role: roleKey,
              icon: this.mdiToDataUri(iconPath, 56, '#064E3B')
            }
          });
          elements.push({ data: { id: `${centerId}-${pid}`, source: centerId, target: pid } });
        });
      };

      addPersonNodes(this.dialogData.persons_author, 'persons_author', mdiFileSign);
      addPersonNodes(this.dialogData.persons_recipient, 'persons_recipient', mdiEmailNewsletter);
      addPersonNodes(this.dialogData.persons_referenced, 'persons_referenced', mdiCommentAccountOutline);

      // Institutionen
      if (Array.isArray(this.dialogData.referenced_companies)) {
        this.dialogData.referenced_companies.forEach((c, i) => {
          const name = typeof c === 'string' ? c : c.name || c.label || JSON.stringify(c);
          const cid = `company-${i}-${name}`.replace(/\s+/g, '_');
          elements.push({
            data: { id: cid, label: name, type: 'company', icon: this.mdiToDataUri(mdiDomain, 56, '#0B5FFF') }
          });
          elements.push({ data: { id: `${centerId}-${cid}`, source: centerId, target: cid } });
        });
      }

      // Referenzierte Dokumente
      if (Array.isArray(this.dialogData.referenced_documents)) {
        this.dialogData.referenced_documents.forEach((d, i) => {
          // nehme an, d enthält ein id oder title/label
          const label = d.title || d.archive_document || d.id || String(d);
          const did = `refdoc-${i}-${label}`.replace(/\s+/g, '_');
          elements.push({
            data: { id: did, label, type: 'document_ref', refId: d.id || d, icon: this.mdiToDataUri(mdiLinkBoxVariantOutline, 56, '#6B21A8') }
          });
          elements.push({ data: { id: `${centerId}-${did}`, source: centerId, target: did } });
        });
      }

      // Erzeuge Cytoscape
      this.cyInstance = cytoscape({
        container: this.$refs.cy,
        elements,
        style: [
          {
            selector: 'node',
            style: {
              'background-color': '#fff',
              'background-fit': 'contain',
              'background-image': 'data(icon)',
              'border-color': '#e5e7eb',
              'border-width': 1,
              'width': 56,
              'height': 56,
              'padding': 6,
              'shape': 'ellipse',
              'label': 'data(label)',
              'text-valign': 'bottom',
              'text-halign': 'center',
              'text-margin-y': 6,
              'font-size': 14,
              'text-wrap': 'wrap',
              'text-max-width': 80
            }
          },
          {
            selector: 'edge',
            style: {
              'width': 2,
              'line-color': '#e5e7eb',
              'curve-style': 'bezier',
              'target-arrow-shape': 'none'
            }
          },
          {
            selector: '[type = "document_center"]',
            style: {
              'background-color': '#fff',
              'width': 70,
              'height': 70,
              'font-weight': 600
            }
          }
        ],
        layout: {
          name: 'cose-bilkent',
          nodeDimensionsIncludeLabels: true,
          animate: true,
          idealEdgeLength: 80,
          nodeRepulsion: 8000,
          gravity: 0.4
        }
      });

      // Klick-Handling: führt applyFilter mit dem passenden key/value aus
      this.cyInstance.on('tap', 'node', (evt) => {
        const d = evt.target.data();
        if (!d) return;

        if (d.type === 'document_center') {
          // bei Klick auf das Zentrum: nimm Dokument-ID und öffne Details (z.B. reload oder neue Suche)
          this.showItemDialog(this.dialogData.id);
          return;
        }

        if (d.type === 'person') {
          // Rolle bestimmt den rkey
          const rkey = d.role || 'persons_referenced';
          this.applyFilter(rkey, d.label);
          return;
        }

        if (d.type === 'company') {
          this.applyFilter('referenced_companies', d.label);
          return;
        }

        if (d.type === 'document_ref') {
          // wenn refId vorhanden, filter nach id sonst nach label
          const val = d.refId || d.label;
          this.applyFilter('referenced_documents', val);
          return;
        }
      });

      // Zoom und Pan anfänglich passend setzen
      this.cyInstance.fit(50);
    },

    applyFilter(key, value) {
      const params = new URLSearchParams(window.location.search);
      console.log(window.location.pathname);
      params.set(key, value);
      const newUrl = `/plus/idsgg/search?${params.toString()}`;
      window.history.pushState({}, '', newUrl);
      window.location.reload();
    }
  },

  watch: {
    dialog(val) {
      if (val) {
        // Dialog geöffnet -> Graph initialisieren nachdem DOM aktualisiert wurde
        this.$nextTick(() => this.initGraph());
      } else {
        if (this.cyInstance) {
          this.cyInstance.destroy();
          this.cyInstance = null;
        }
      }
    }
  }
}
</script>

<style scoped>
.btn {
  border-bottom: 1px dotted #000;
  background-color: white;
  cursor: pointer;
  font-weight: 200;
  margin: 0px 10px 15px 20px;
  text-align: left;
}

.btn:hover {
  background-color: rgb(229, 231, 235);
}

.btnChip {
  background-color: rgb(255, 255, 255);
  cursor: pointer;
}

.btnChip:hover {
  background-color: rgb(229, 231, 235);
}
</style>