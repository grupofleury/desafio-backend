import App from './app'
const port = parseInt(<string>process.env.PORT) || 4446
App.start(port)