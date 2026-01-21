export default defineNuxtPlugin(() => {
  if (process.server) return;

  // init _paq
  (window as any)._paq = (window as any)._paq || [];
  const _paq = (window as any)._paq;
  _paq.push(['trackPageView']);
  _paq.push(['enableLinkTracking']);
  _paq.push(['setTrackerUrl', 'https://stats.ids-mannheim.de/piwik.php']);
  _paq.push(['setSiteId', '6']);

  // load piwik.js
  const d = document;
  const g = d.createElement('script');
  const s = d.getElementsByTagName('script')[0];
  g.type = 'text/javascript';
  g.async = true;
  g.defer = true;
  g.src = 'https://stats.ids-mannheim.de/piwik.js';
  s.parentNode?.insertBefore(g, s);

  // track on SPA route changes
  const router = useRouter();
  router.afterEach(() => {
    _paq.push(['setCustomUrl', window.location.href]);
    _paq.push(['setDocumentTitle', document.title]);
    _paq.push(['trackPageView']);
  });

  // optional: expose helper
  return {
    provide: {
      piwik: {
        trackEvent: (category: string, action: string, name?: string, value?: number) =>
          _paq.push(['trackEvent', category, action, name, value])
      }
    }
  };
});