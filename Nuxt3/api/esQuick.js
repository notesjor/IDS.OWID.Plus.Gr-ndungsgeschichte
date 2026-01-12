export default class esQuick {
  constructor() {
    this.__baseUrl = "http://localhost:9200";
    this.__index = "owid";
  }

  initialize(baseUrl, indexName) {
    this.__baseUrl = baseUrl;
    this.__index = indexName;
  }

  getDocumentById(id, func) {
    var url = `${this.__baseUrl}/${this.__index}/_doc/${id}`;

    var myHeaders = new Headers();
    var requestOptions = {
      method: "GET",
      headers: myHeaders,
      redirect: "follow",
    };

    fetch(url, requestOptions)
      .then((response) => {
        if (response.status != 200) throw new Error("ES-Request failed");
        return response;
      })
      .then((response) => response.json())
      .then((json) => {
        func(json);
      })
      .catch((error) => {
        console.log("error", error);
        func(null);
      });
  }

  easySearch(query, from, size, func) {
    if (size == undefined) size = 10;
    if (size < 1) size = 1;
    if (from == undefined || from < 0) from = 0;

    var url = `${this.__baseUrl}/${this.__index}/_search?q=${query}&from=${from}&size=${size}`;

    var myHeaders = new Headers();
    var requestOptions = {
      method: "GET",
      headers: myHeaders,
      redirect: "follow",
    };

    fetch(url, requestOptions)
      .then((response) => {
        if (response.status != 200) throw new Error("ES-Request failed");
        return response;
      })
      .then((response) => response.json())
      .then((json) => {
        func(json);
      })
      .catch((error) => {
        console.log("error", error);
        func(null);
      });
  }

  easySearchGetMax(data) {
    return data.hits.total.value;
  }

  easySearchGetHits(data) {
    console.log(data);
    return data.hits.hits;
  }
}
