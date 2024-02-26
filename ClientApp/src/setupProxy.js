const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:59111';

const context =  [
  '/api' 
];

function onError(err) {
  console.error(err.message);
}

module.exports = function(app) {
  const appProxy = createProxyMiddleware(context, {
    proxyTimeout: 10000,
    onError,
    target: target,
    secure: false,
    changeOrigin: true,
    ws: true
  });

  app.use(appProxy);
};
