<script setup>
useHead({
    title: "IDS‑Gründungsgeschichte: Team"
})
</script>

<template>
    <projectHeader />
    <div style="max-width: 80ch; margin-left: auto; margin-right: auto;">
        <p style="margin-top:20px">
            Das vorliegende Spezialarchiv wurde im Rahmen des Projekts
            “In-Wert-Setzungen von Sprache am Institut für Deutsche Sprache zu
            seiner Gründungszeit” erstellt, das vom Leibniz-Forschungsverbund 
            <a href="https://www.leibniz-wert-der-vergangenheit.de/">Wert der Vergangenheit</a>
            in den Jahren 2024 und 2025 gefördert wurde.
        </p>
        <div style="margin-top:10px">
            <h3>Projektteam:</h3>
            <v-list>
                <v-list-item>
                    <div>
                        <a href="https://www.ids-mannheim.de/personal/seiten/dang.html">Mark Dang-Anh</a>
                    </div>
                </v-list-item>
                <v-list-item>
                    <div>
                        <a href="https://www.ids-mannheim.de/personal/seiten/scholl.html">Stefan Scholl</a>
                    </div>
                </v-list-item>                
                <v-list-item>
                    <div>
                        <a href="https://www.ids-mannheim.de/personal/seiten/mueller_carolin.html">Carolin
                            Müller-Spitzer</a>
                    </div>
                </v-list-item>
                <v-list-item>
                    <div>
                        <a href="https://www.ids-mannheim.de/lexik/personal/">Lea Waßmer</a>
                    </div>
                </v-list-item>
            </v-list>
            <p>Technische Umsetzung:</p>
            <v-list>
                <v-list-item>
                    <div>
                        <a href="https://perso.ids-mannheim.de/seiten/ruediger.html">Jan Oliver Rüdiger</a>
                    </div>
                </v-list-item>
            </v-list>
        </div>
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
                { item: 'Autor A' },
                { item: 'Autor B' },
                { item: 'Autor C' },
                { item: 'Autor D' },
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