<script setup>
useHead({
  title: "IDS‑Gründungsgeschichte: Findbuch"
})
</script>

<template>
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto;">
      <h1 style="margin-top:1rem">Findbuch</h1>
    </v-col>
  </v-row>
  <v-row>
    <v-col style="max-width:80ch; margin-left:auto; margin-right:auto; padding-bottom: 2rem; margin-top: -20px;">
      Das Findbuch umfasst selektierte Dokumente des IDS-Vorstandsarchivs
      der Jahre 1963–1971. Als Download zur Verfügung gestellt werden
      ausschließlich nicht-persönliche Sachakten. Personenbezogene Dokumente
      können nur auf Anfrage vor Ort im Gebäude des IDS in Mannheim gesichtet
      werden. Auf der linken Seite finden Sie verschiedene Kategorien, über
      die Sie gezielt suchen können. In der Fußleiste jedes Eintrags sehen
      Sie, wie viele Personen oder Institutionen mit dem jeweiligen Text
      verknüpft sind. Durch einen Klick auf einen Text erhalten Sie
      zusätzliche Detailinformationen, wie z.B. eine grafische Anzeige der mit
      diesem Dokument verknüpften Entitäten (Personen, Institutionen
      etc.).
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
            <paperstack :source="v" @callback="showItemDialog" :locked="locked.has(v.id)" />
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
            <div v-if="locked.has(dialogData.id)">
              <v-icon size="64px" style="margin: 65px 0px 10px 45px;">mdi-lock-outline</v-icon>
              <p>Zugriff nur vor Ort</p>
            </div>
            <div v-else>
              <img :src="`/plus/idsgg/thumbnail/${dialogData.id}.jpg`" alt="Bild auf oberstem Blatt"
                style="max-height: 300px; max-width: 480px; margin-left:auto; margin-right: auto;" />
              <v-btn prepend-icon="mdi-download-circle-outline" style="margin:5px 0px" v-if="dialogData.has_fulltext"
                :href="`/plus/idsgg/fulltext/${dialogData.id}.pdf`">
                Download</v-btn>
            </div>
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
          <span>Verknüpfte Entitäten (Visualisierung):</span>
          <span style="font-size: 0.9rem; font-weight: 200;">Hinweis: Ein Doppelklick auf eine Entität filtert alle
            Dokumente. Graph mit Mausrand zoombar.</span>
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

  <scrollUp />
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

      cyInstance: null,
      locked: new Set(["04_26_", "04_25_", "04_02_01", "04_02_02", "04_02_03", "04_02_04", "04_02_05", "04_04_01", "04_04_02", "04_04_03", "04_04_04", "04_04_05", "04_04_06", "04_04_07", "04_04_08", "04_04_09", "04_04_10", "04_04_11", "04_04_12", "04_04_13", "04_04_14", "04_04_15", "04_04_16", "04_04_17", "04_04_18", "04_04_19", "04_22_02", "04_22_03", "04_22_05", "04_23_01", "04_23_02", "04_23_03", "04_23_04", "04_23_05", "04_23_06", "04_23_07", "04_23_08", "04_23_09", "04_23_10", "04_23_11", "04_23_12", "04_25_0", "04_26_0", "04_26_01", "04_26_02", "04_26_03", "04_54_01", "04_54_02", "04_54_05", "04_55_04", "04_55_08", "04_55_09", "04_55_10", "04_55_12", "04_55_14", "04_55_15", "04_55_16", "04_55_18", "04_55_19", "04_55_20", "04_55_22", "04_55_23", "04_55_24", "05_85a_01", "05_85a_02", "05_85a_04", "05_85a_05", "05_85a_06", "05_85a_07", "06_124_0", "06_126_0", "06_137_02", "06_137_03", "06_137_04", "06_137_05", "06_137_06", "06_137_07", "06_137_08", "06_137_09", "06_137_10", "06_137_12", "06_137_13", "06_137_14", "06_137_15", "06_137_16", "06_137_17", "07_211_01", "07_211_02", "07_211_03", "07_217_0", "08_252_01", "08_252_02", "08_252_03", "08_252_04", "08_252_05", "08_252_06", "08_252_07", "08_252_08", "08_252_09", "08_252_10", "08_252_11", "08_252_12", "08_252_13", "08_252_15", "08_252_16", "08_264_01", "08_264_02", "08_264_03", "08_264_04", "08_264_05", "08_264_06", "08_264_07", "08_264_08", "08_264_09", "08_267_0", "08_268_0", "08_271_0", "08_272_0", "08_277_0", "09_281_0", "09_282_01", "09_282_02", "09_282_03", "09_282_04", "09_282_05", "09_282_06", "09_282_07", "09_282_08", "09_282_09", "09_282_10", "09_282_11", "09_282_12", "09_282_14", "09_282_17", "09_287_02", "09_288_0", "09_290_0", "10_355_0", "10_385_01", "10_385_02", "10_385_03", "10_385_04", "10_385_05", "10_385_06", "10_385_07", "10_385_08", "10_385_09", "10_469_0", "10_470_0", "10_471_0", "10_472_0", "11_446_0", "11_482_01", "11_482_02", "11_482_03", "11_482_04", "11_482_05", "11_482_06", "11_482_07", "11_482_08", "11_482_09", "11_482_10", "11_482_11", "11_482_12", "11_482_13", "11_482_14", "11_486_0", "11_487_0", "11_488_0", "11_490_0", "11_491_0", "11_492_0", "11_493_0", "11_494_0", "11_495_0", "12_512_0", "12_514x_0", "12_516_0", "12_521_01", "12_521_02", "12_521_03", "12_522_0", "12_552_0"])
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
    // Hilfsfunktion: SVG aus mdi-Pfad erzeugen und als data-uri zurückgeben. Optional kann ein weißer Hintergrund angegeben werden.
    mdiToDataUri(path, size = 48, color = '#222', bg = 'none') {
      const bgRect = bg && bg !== 'none' ? `<rect width="24" height="24" fill="${bg}"/>` : '';
      const svg = `<svg xmlns="http://www.w3.org/2000/svg" width="${size}" height="${size}" viewBox="0 0 24 24">${bgRect}<path fill="${color}" d="${path}"/></svg>`;
      return `data:image/svg+xml;charset=utf8,${encodeURIComponent(svg)}`;
    },


    // Initialisiert / (re)drawt den Cytoscape-Graphen
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

      // Gruppen/Verteilknoten: für jede Entitätstyp-Gruppe (Autor*innen, Adressat*innen, Sonst. Personen, Institutionen, Dokumente)
      const addGroupNode = (groupKey, label, iconPath, color = '#064E3B') => {
        const gid = `${centerId}-group-${groupKey}`;
        elements.push({ data: { id: gid, label, type: 'group', groupKey, icon: this.mdiToDataUri(iconPath, 56, color, '#fff') } });
        elements.push({ data: { id: `${centerId}-${gid}`, source: centerId, target: gid } });
        return gid;
      };

      // Detail-Knoten werden ohne Icon angelegt und von den Verteilknoten abgehängt
      const addPersonDetails = (arr, roleKey) => {
        if (!Array.isArray(arr) || arr.length === 0) return;
        const groupId = addGroupNode(roleKey,
          roleKey === 'persons_author' ? 'Verfasser*innen' : roleKey === 'persons_recipient' ? 'Adressat*innen' : 'Sonstige referenzierte Personen',
          roleKey === 'persons_author' ? mdiFileSign : roleKey === 'persons_recipient' ? mdiEmailNewsletter : mdiCommentAccountOutline,
          '#000'
        );
        arr.forEach((p, idx) => {
          const name = typeof p === 'string' ? p : p.name || p.label || JSON.stringify(p);
          const pid = `person-detail-${roleKey}-${idx}-${name}`.replace(/\s+/g, '_');
          elements.push({ data: { id: pid, label: name, type: 'person_detail', rkey: roleKey } });
          elements.push({ data: { id: `${groupId}-${pid}`, source: groupId, target: pid } });
        });
      };

      addPersonDetails(this.dialogData.persons_author, 'persons_author');
      addPersonDetails(this.dialogData.persons_recipient, 'persons_recipient');
      addPersonDetails(this.dialogData.persons_referenced, 'persons_referenced');

      // Institutionen
      if (Array.isArray(this.dialogData.referenced_companies) && this.dialogData.referenced_companies.length > 0) {
        const groupId = addGroupNode('referenced_companies', 'Referenzierte Institutionen', mdiDomain, '#000');
        this.dialogData.referenced_companies.forEach((c, i) => {
          const name = typeof c === 'string' ? c : c.name || c.label || JSON.stringify(c);
          const cid = `company-detail-${i}-${name}`.replace(/\s+/g, '_');
          elements.push({ data: { id: cid, label: name, type: 'company_detail', rkey: 'referenced_companies' } });
          elements.push({ data: { id: `${groupId}-${cid}`, source: groupId, target: cid } });
        });
      }

      // Referenzierte Dokumente
      if (Array.isArray(this.dialogData.referenced_documents) && this.dialogData.referenced_documents.length > 0) {
        const groupId = addGroupNode('referenced_documents', 'Referenzierte Dokumente', mdiLinkBoxVariantOutline, '#000');
        this.dialogData.referenced_documents.forEach((d, i) => {
          const label = d.title || d.archive_document || d.id || String(d);
          const did = `refdoc-detail-${i}-${label}`.replace(/\s+/g, '_');
          elements.push({ data: { id: did, label, type: 'document_ref_detail', refId: d.id || d } });
          elements.push({ data: { id: `${groupId}-${did}`, source: groupId, target: did } });
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
              'font-size': 16,
              'text-wrap': 'wrap',
              'text-max-width': 80
            }
          },
          {
            selector: '[type = "group"]',
            style: {
              'background-color': '#fff',
              'background-image': 'data(icon)',
              'background-fit': 'contain',
              'background-clip': 'none',
              'background-image-opacity': 1,
              'background-width': 56,
              'background-height': 56,
              'border-width': 0,
              'width': 56,
              'height': 56,
              'padding': 0,
              'shape': 'rectangle',
              'label': 'data(label)',
              'text-valign': 'bottom',
              'text-halign': 'center',
              'text-margin-y': 6,
              'font-size': 16,
              'text-wrap': 'wrap',
              'text-max-width': 140
            }
          },
          {
            selector: '[type = "person_detail"], [type = "company_detail"], [type = "document_ref_detail"]',
            style: {
              'background-image': 'none',
              'background-color': '#fff',
              'background-opacity': 1,
              'border-color': '#e5e7eb',
              'border-width': 1,
              'width': 'label',
              'height': 'label',
              'padding': 8,
              'shape': 'roundrectangle',
              'label': 'data(label)',
              'text-valign': 'center',
              'text-halign': 'center',
              'text-margin-y': 0,
              'font-size': 16,
              'text-wrap': 'wrap',
              'text-max-width': 240,
              'color': '#000',
              'cursor': 'pointer'
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
          // bei Klick auf das Zentrum: nimm Dokument-ID und öffne Details
          this.showItemDialog(this.dialogData.id);
          return;
        }

        // Klick auf Gruppen-Knoten: nichts 
        if (d.type === 'group') {
          return; // optional: implement expand/collapse
        }

        // Detailknoten für Personen
        if (d.type === 'person_detail') {
          const rkey = d.rkey || 'persons_referenced';
          this.applyFilter(rkey, d.label);
          return;
        }

        // Detailknoten für Institutionen
        if (d.type === 'company_detail') {
          this.applyFilter('referenced_companies', d.label);
          return;
        }

        // Detailknoten für referenzierte Dokumente
        if (d.type === 'document_ref_detail') {
          const val = d.refId || d.label;
          this.applyFilter('referenced_documents', val);
          return;
        }

        // Fallback
        if (d.type === 'person') {
          const rkey = d.role || 'persons_referenced';
          this.applyFilter(rkey, d.label);
          return;
        }

        if (d.type === 'company') {
          this.applyFilter('referenced_companies', d.label);
          return;
        }

        if (d.type === 'document_ref') {
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