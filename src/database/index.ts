import * as path from 'path';
import { Sequelize } from 'sequelize-typescript';

/**
 * Responsavel pela conexao com banco de dados
 */
class Database {
  constructor() {}

  public async connect(): Promise<Sequelize> {
    const dbconn = new Sequelize({
      dialect: process.env.DB_DIALECT || 'sqlite',
      host: process.env.DB_HOST,
      port: parseInt(process.env.DB_DATABASE) || 5432,
      database: process.env.DB_DATABASE,
      username: process.env.DB_USERNAME,
      password: process.env.DB_PASSWORD,
      storage: './db.sqlite',
      operatorsAliases: false,
      logging: false,
      pool: {
        max: 5,
        min: 0,
        acquire: 30000,
        idle: 10000
      },
      modelPaths: [path.normalize(`${__dirname}/../models`)]
    });

    await dbconn.authenticate();

    await dbconn.sync({});

    return dbconn;
  }
}
export default Database;
