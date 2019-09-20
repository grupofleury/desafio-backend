console.log(process.env.DATABASE_NAME)
module.exports = {
  name: 'default',
  type: 'sqlite',
  database: process.env.DATABASE_NAME,
  entities: ['src/entities/*.ts', 'src/entities/*.js'],
  migrations: ['migrations/*.ts', 'migrations/*.js'],
  cli: {
    migrationsDir: 'migrations'
  }
}
