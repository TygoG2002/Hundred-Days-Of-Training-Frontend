const CACHE_NAME = 'hundreddays-static-v1';

const STATIC_ASSETS = [
    '/',
    '/index.html',
    '/favicon.ico',
    '/manifest.webmanifest',
    '/HundredDays.styles.css',
    '/css/app.css',
];

self.addEventListener('install', event => {
    event.waitUntil(
        caches.open(CACHE_NAME).then(cache => {
            return cache.addAll(STATIC_ASSETS);
        })
    );
    self.skipWaiting();
});

self.addEventListener('activate', event => {
    event.waitUntil(
        caches.keys().then(keys =>
            Promise.all(
                keys
                    .filter(k => k !== CACHE_NAME)
                    .map(k => caches.delete(k))
            )
        )
    );
    self.clients.claim();
});

self.addEventListener('fetch', event => {
    const req = event.request;

    if (req.url.includes('/api/')) {
        return;
    }

    event.respondWith(
        caches.match(req).then(cached => {
            return cached || fetch(req);
        })
    );
});
