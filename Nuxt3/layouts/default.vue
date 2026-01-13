<template>
  <v-app>
    <v-app-bar app theme="dark" class="d-print-none" style="z-index: 99999999">
      <div>
        <a :href="leftIconHref" target="_blank">
          <img alt="Logo" src="/logo_left.svg" style="min-height:40px; margin-left:10px" />
        </a>
      </div>

      <v-spacer></v-spacer>

      <div>
        <a :href="rightIconHref" target="_blank">
          <img alt="Logo" src="/logo_right.svg" style="min-height:70px" />
        </a>
      </div>
    </v-app-bar>   

    <v-navigation-drawer expand-on-hover permanent rail>
      <v-list>
      <v-list-item href="/" title="Übersicht" prepend-icon="mdi-home"></v-list-item>
      <v-list-item href="/search" title="Findebuch" prepend-icon="mdi-text-box-search-outline"></v-list-item>
      <v-list-item href="/browse" title="Verzeichnis" prepend-icon="mdi-file-cabinet"></v-list-item>
      <v-list-item href="/team" title="Projektteam" prepend-icon="mdi-account-group-outline"></v-list-item>      
      </v-list>
    </v-navigation-drawer>

    <v-main style="margin-bottom: 100px;">
      <slot />
    </v-main>

    <v-footer theme="dark"
      style="z-index: 99999999; max-height: 80px; position: absolute; bottom: 0; width: 100%; padding-left:25px; display: grid; grid-template-columns: 1fr 1fr 1fr; grid-template-rows: 100%; gap: 0px 0px; grid-template-areas: 'left middle right';">
      <div style="color:white; grid-area: left; margin-top: 20px; font-size: 12px;">
        <img alt="Logo" src="/owid-logo-dunkel.svg" style="max-height:15px; margin:-10px 0px 3px 0px" float="left" />
        <div>
          <a style="color: #fff" :href="footerContact">Kontakt</a>
          &middot;
          <a style="color: #fff" :href="footerDsgvo">Datenschutzhinweis</a>
          &middot;
          <a style="color: #fff" :href="footerImpressum">Impressum</a>
        </div>
        <div>
          &copy; Leibniz-Institut für Deutsche Sprache
        </div>
      </div>

      <div style="grid-area: middle;"></div>

      <div style="text-align: right; grid-area: right">
        <a :href="rightIconHref" target="_blank">
          <img alt="Logo" src="/logo_right.svg"
            style="max-height:65px; min-height: 45px; min-width: 200px; margin-left: auto; " />
        </a>
      </div>
    </v-footer>    
  </v-app>
</template>

<script>
export default {
  name: "Index",
  theme: { dark: false },
  data() {
    return {
      appName: null,
      appDescription: null,

      leftIconHref: null,
      rightIconHref: null,

      footerContact: null,
      footerImpressum: null,
      footerDsgvo: null,
    }
  },

  mounted() {
    this.appName = this.$config.public.appName;
    this.appDescription = this.$config.public.appDescription;

    this.leftIconHref = this.$config.public.leftIconHref;
    this.rightIconHref = this.$config.public.rightIconHref;

    this.footerContact = this.$config.public.footerContact;
    this.footerImpressum = this.$config.public.footerImpressum;
    this.footerDsgvo = this.$config.public.footerDsgvo;
  },

  methods: {
    test() {
      alert("test");
    }
  }
}
</script>

<style></style>