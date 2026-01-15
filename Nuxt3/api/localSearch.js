import FlexSearch from "flexsearch";

export class BaseMapping {
  constructor(property, options = {}) {
    this.property = property;
    this.options = options;
  }

  collect(item, list) {
    throw new Error("collect() not implemented");
  }

  matches(item, queryValue) {
    return true;
  }
}

/* =====================
   KEYWORD
   ===================== */
export class KeywordMapping extends BaseMapping {
  collect(item, list) {
    const raw = item[this.property];
    if (!raw) return;

    const values = Array.isArray(raw) ? raw : [raw];
    values.forEach((v) => list.add(v));
  }

  matches(item, queryValue) {
    if (!queryValue || queryValue.length === 0) return true;

    const raw = item[this.property];
    if (!raw) return false;

    const itemValues = new Set(Array.isArray(raw) ? raw : [raw]);

    const queries = Array.isArray(queryValue) ? queryValue : [queryValue];

    for (const q of queries) {
      if (itemValues.has(q)) return true;
    }

    return false;
  }
}

/* =====================
   RANGE (integer)
   ===================== */
export class RangeMapping extends BaseMapping {
  collect(item, list) {
    const value = item[this.property];
    if (typeof value !== "number") return;

    list.min = list.min === undefined ? value : Math.min(list.min, value);
    list.max = list.max === undefined ? value : Math.max(list.max, value);
  }

  matches(item, queryValue) {
    if (!queryValue) return true;

    const value = item[this.property];
    if (typeof value !== "number") return false;

    const { from, to } = queryValue;
    if (from !== undefined && value < from) return false;
    if (to !== undefined && value > to) return false;

    return true;
  }
}

/* =====================
   TREE (group/item)
   ===================== */
export class TreeMapping extends BaseMapping {
  constructor(property, options = {}) {
    super(property, options);
    this.subProperty = options.sub;
    if (!this.subProperty) {
      throw new Error(`TreeMapping requires 'sub' option`);
    }
  }

  collect(item, list) {
    const group = item[this.property];
    const entry = item[this.subProperty];

    if (!group || !entry) return;

    if (!list[group]) {
      list[group] = new Set();
    }

    list[group].add(entry);
  }

  matches(item, queryValue) {
    if (!queryValue) return true;

    const group = item[this.property];
    const entry = item[this.subProperty];
    if (!group || !entry) return false;

    return Object.entries(queryValue).some(([qGroup, qEntries]) => {
      if (qGroup !== group) return false;

      const entries = Array.isArray(qEntries) ? qEntries : [qEntries];

      return entries.includes(entry);
    });
  }
}

/* =====================
   BOOL
   ===================== */
export class BoolMapping extends BaseMapping {
  collect(item, list) {
    // do nothing
  }

  matches(item, queryValue) {
    if (queryValue === undefined) return true;
    return item[this.property] === queryValue;
  }
}

/* =====================
   DICT (kombinierte Keys)
   ===================== */
export class DictMapping extends BaseMapping {
  collect(item, list) {
    const parts = this.options.parts.map((p) => item[p]);
    if (parts.some((p) => p === undefined)) return;

    const key = parts.join(this.options.separator || "|");
    if (!list[key]) list[key] = parts;
  }

  matches(item, queryValue) {
    if (!queryValue || queryValue.length === 0) return true;

    const parts = this.options.parts.map((p) => item[p]);
    const key = parts.join(this.options.separator || "|");

    return queryValue.includes(key);
  }
}

/* =====================
   FULLTEXT (Volltext-Index mit flexsearch)
   ===================== */
export class FulltextMapping extends BaseMapping {
  constructor(property, options = {}) {
    super(property, options);

    // Key-Property selbst vergeben
    this.keyProperty = options.key;
    if (!this.keyProperty) {
      throw new Error("FulltextMapping requires 'key' option");
    }

    // FlexSearch Index im Document-Modus
    this.index = new FlexSearch.Document({
      encode: options.encode || "advanced",
      tokenize: options.tokenize || "forward",
      language: "de",
      doc: {
        id: this.keyProperty, // wir verwenden das selbstgewählte Key-Feld als ID
        field: [property], // nur das gegebene Property wird indiziert
      },
      cache: true,
    });

    // interne Map, falls wir zusätzliche Infos brauchen
    this.itemsMap = new Map();
  }

  // Item zum Index hinzufügen
  collect(item) {
    var value = item[this.property];
    if (!value) return;

    var key = item[this.keyProperty];
    if (!key) return;

    // In interne Map speichern
    this.itemsMap.set(key, item);

    // FlexSearch Index
    this.index.add(item);
  }

  // Prüfen, ob ein Item zum Query passt
  matches(item, queryValue) {
    if (!queryValue || !queryValue.trim()) return true;

    var key = item[this.keyProperty];
    var results = this.index.search(queryValue, { field: this.property });

    // results ist ein Array von Arrays pro Feld → prüfen, ob Key enthalten ist
    return results.some(function (r) {
      return r.result.includes(key);
    });
  }

  // Alle Treffer zurückgeben
  search(queryValue) {
    if (!queryValue || !queryValue.trim())
      return Array.from(this.itemsMap.values());

    var results = this.index.search(queryValue, { field: this.property });
    // alle Keys zusammenführen
    var keys = results.flatMap(function (r) {
      return r.result;
    });

    // Map → Items
    return keys.map((k) => this.itemsMap.get(k));
  }
}

/* =====================
   LOCAL SEARCH
   ===================== */
export class LocalSearch {
  list;
  query;

  constructor({ data = [], mapping = {} }) {
    this.data = data;
    this.mappings = {};
    this.list = {};
    this.query = {};

    this._initMappings(mapping);
    this._collectLists();
  }

  _initMappings(mapping) {
    Object.entries(mapping).forEach(([prop, config]) => {
      let instance;

      switch (config.type) {
        case "keyword":
          instance = new KeywordMapping(prop, config);
          this.list[prop] = new Set();
          break;

        case "range":
          instance = new RangeMapping(prop, config);
          this.list[prop] = {};
          break;

        case "tree":
          instance = new TreeMapping(prop, config);
          this.list[prop] = {};
          break;

        case "bool":
          instance = new BoolMapping(prop, config);
          this.list[prop] = [true, false];
          break;

        case "dict":
          instance = new DictMapping(prop, config);
          this.list[prop] = {};
          break;
        
        case "fulltext":
          instance = new FulltextMapping(prop, config);
          this.list[prop] = {};
          break;

        default:
          throw new Error(`Unknown mapping type: ${config.type}`);
      }

      this.mappings[prop] = instance;
    });
  }

  _collectLists() {
    this.data.forEach((item) => {
      Object.entries(this.mappings).forEach(([prop, mapper]) => {
        mapper.collect(item, this.list[prop]);
      });
    });
  }

  filter() {
    return this.data.filter((item) => {
      return Object.entries(this.mappings).every(([prop, mapper]) => {
        return mapper.matches(item, this.query[prop]);
      });
    });
  }
}

/* =====================
   EXPORT
   ===================== */
export default LocalSearch;
