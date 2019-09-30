import express from 'express';
import swaggerUi from 'swagger-ui-express';
import routes from './routes/index';
import { config } from 'dotenv';
import { resolve } from 'path';
import * as swaggerDocument from '../swagger.json';

import Database from './database/index';
class App {
  public app: express.Application;
  public db: any;
  constructor() {
    this.app = express();
    config({
      path: resolve(__dirname, '../', '.env')
    });
    this.db = new Database().connect();

    this.middlewares();
    this.routes();
  }

  routes(): void {
    this.app.use(routes);
  }

  middlewares() {
    this.app.use(express.json());
    this.app.use('/docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));
  }

  listen(PORT: number) {
    this.app.listen(PORT, () =>
      console.log(`Server it's running at port: ${PORT}`)
    );
  }
}
export default new App();
