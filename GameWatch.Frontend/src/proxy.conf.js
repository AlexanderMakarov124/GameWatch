const PROXY_CONFIG = [
  {
    context: [
      '/api/games',
      '/api/gameLists'
    ],
    target: 'https://localhost:7211',
    secure: false
  }
];

module.exports = PROXY_CONFIG;
