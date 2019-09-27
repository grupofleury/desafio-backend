import * as path from "path";
import { Sequelize } from "sequelize-typescript";
import { config } from './config'

/**
 * Responsavel pela conexao com banco de dados
 */
class Database {
  constructor() {
    this.connect();
  }

  public async connect(): Promise<Sequelize> {
    const dbconn = new Sequelize({
      ...config[process.env.NODE_ENV === 'dev' ? 'dev' : 'prod'],
      operatorsAliases: false,
      logging: false,
      pool: {
        max: 5,
        min: 0,
        acquire: 30000,
        idle: 10000
      },
      modelPaths: [path.normalize(`${__dirname}/../models`)],
      
    });

    await dbconn.authenticate();

    await dbconn.sync({  });

    return dbconn;
  }
}
export default new Database();
