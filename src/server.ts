import * as express from 'express';

class App {
  public app: express.Application;
  router: express.Router;
  constructor() {
    this.app = express();
    this.routes()
  }

  routes() {
    this.router = express.Router();

    this.router.get('/', (req, res) => {
      res.send('ok')
    })
    this.app.use(this.router)

  }

  listen(PORT){
    this.app.listen(PORT, () => console.log(`Server it's running at port: ${PORT}`))
  }
}
export default new App()
