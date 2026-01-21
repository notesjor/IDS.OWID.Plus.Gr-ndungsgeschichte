<script setup>
useHead({
  title: "IDS‑Gründungsgeschichte: Team"
})
</script>

<template>
  <projectHeader/>
  <div style="max-width: 80ch; margin-left: auto; margin-right: auto;">
    <p style="margin-top:20px">
        Lorem ipsum dolor, sit amet consectetur adipisicing elit. Iusto facere nulla nam ducimus sed sint porro, aut voluptatem labore quo dignissimos officiis pariatur veniam facilis expedita doloribus eos temporibus dolorem?
    </p>
    <div style="margin-top:10px">
        <h3>Projektteam</h3>
        <v-list>
            <v-list-item>
                <div>
                    <a href="https://www.ids-mannheim.de/personal/seiten/scholl.html">Stefan Scholl</a>
                </div>                
                <div style="font-weight: 200; font-size: 0.9rem; margin-left: 20px;">
                    Projektleitung, Koordination, Auswertung und Analyse der Daten
                </div>
            </v-list-item>
            <v-list-item>
                <div>
                    <a href="https://www.ids-mannheim.de/personal/seiten/dang.html">Mark Dang-Anh</a>
                </div>                
                <div style="font-weight: 200; font-size: 0.9rem; margin-left: 20px;">
                    Projektmitarbeit, Auswertung und Analyse der Daten
                </div>
            </v-list-item>
            <v-list-item>
                <div>
                    <a href="https://www.ids-mannheim.de/personal/seiten/mueller_carolin.html">Carolin Müller-Spitzer</a>
                </div>                
                <div style="font-weight: 200; font-size: 0.9rem; margin-left: 20px;">
                    Projektmitarbeit, Auswertung und Analyse der Daten
                </div>
            </v-list-item>
            <v-list-item>
                <div>
                    <a href="https://www.ids-mannheim.de/lexik/personal/">Lea Waßmer</a>
                </div>                
                <div style="font-weight: 200; font-size: 0.9rem; margin-left: 20px;">
                    Wissenschaftliche Hilfskraft, Digitalisierung, Erhebung und Aufbereitung der Daten
                </div>
            </v-list-item>
            <v-list-item>
                <div>
                    <a href="https://perso.ids-mannheim.de/seiten/ruediger.html">Jan Oliver Rüdiger</a>
                </div>                
                <div style="font-weight: 200; font-size: 0.9rem; margin-left: 20px;">
                    Datenaufbereitung und Softwareentwicklung
                </div>
            </v-list-item>
        </v-list>    
    </div>
    <p style="margin-top:30px">
        Lorem ipsum dolor, sit amet consectetur adipisicing elit. Iusto facere nulla nam ducimus sed sint porro, aut voluptatem labore quo dignissimos officiis pariatur veniam facilis expedita doloribus eos temporibus dolorem?
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Ad itaque amet, eos officia ducimus sint nobis ea perferendis consequatur ab. Ut natus placeat quibusdam! Nobis temporibus sed nostrum amet blanditiis?
    </p>
  </div>
</template>

<script>
export default {
  data() {
    return {
      appName: 'Nuxt 3',
      appDescription: 'The Intuitive Vue Framework',
      panels: [],
      search_dateRange: [1945, 1970],
      inputItems: [
        { item: 'Autor A'},
        { item: 'Autor B'},
        { item: 'Autor C'},
        { item: 'Autor D'},
      ],
    }
  },
  mounted() {
    this.appName = this.$config.public.appName;
    this.appDescription = this.$config.public.appDescription;
  },
}
</script>

<style scoped>
a {
  text-decoration: none;
  border-bottom: 1px dotted #000;
  background-color: white;
}

a:hover {
  text-decoration: none;
  border-bottom: 1px dotted #000;
  background-color: rgb(229, 231, 235);
}
</style>