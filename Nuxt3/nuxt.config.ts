import svgLoader from "vite-svg-loader"

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  postcss: {
    plugins: {
      tailwindcss: {},
      autoprefixer: {},
    },
  },

  css: [
    'vuetify/lib/styles/main.sass',
    '@mdi/font/css/materialdesignicons.min.css',
    '~/assets/css/main.css'
  ],

  build: {
    transpile: ['vuetify'],
  },

  vite: {
    define: {
      'process.env.DEBUG': false,
    },
    plugins: [svgLoader({
      defaultImport: 'url',
    })]
  },

  modules: ['@pinia/nuxt'],

  runtimeConfig: {
    public: {
      appName: "Gründungsgeschichte",

      langCode: "de",

      leftIconHref: "https://www.owid.de/plus/index.html",
      rightIconHref: "https://www.ids-mannheim.de/",

      footerContact: "mailto:ruediger@ids-mannheim.de",
      footerImpressum: "https://www.owid.de/wb/owid/impressum.html",
      footerDsgvo: "https://www.owid.de/wb/owid/privacy.html"
    }
  },

  app: {
    baseURL: "/plus/idsgg",
    head: {
      title: "Gründungsgeschichte",
      htmlAttrs: { lang: "de" },
      link: [

        { rel: 'preload', href: '/fonts/fira-sans/fira-sans-v11-latin-italic.woff2', as: 'font', type: 'font/woff2', crossorigin: 'anonymous' },
        { rel: 'preload', href: '/fonts/fira-sans/fira-sans-v11-latin-regular.woff2', as: 'font', type: 'font/woff2', crossorigin: 'anonymous' },
        { rel: 'preload', href: '/fonts/fira-sans/fira-sans-v11-latin-300italic.woff2', as: 'font', type: 'font/woff2', crossorigin: 'anonymous' },
        { rel: 'preload', href: '/fonts/fira-sans/fira-sans-v11-latin-300.woff2', as: 'font', type: 'font/woff2', crossorigin: 'anonymous' },
        { rel: 'preload', href: '/fonts/fira-sans/fira-sans-v11-latin-200italic.woff2', as: 'font', type: 'font/woff2', crossorigin: 'anonymous' },
        { rel: 'preload', href: '/fonts/fira-sans/fira-sans-v11-latin-200.woff2', as: 'font', type: 'font/woff2', crossorigin: 'anonymous' }
        
      ]
    }
  },

  compatibilityDate: "2024-12-04"
})