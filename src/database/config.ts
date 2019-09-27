export const config = {
  prod: {
    dialect: "postgres",
    host: "localhost",
    port: 5432,
    database: "fleury",
    username: "postgres",
    password: "docker",
  },
  dev: {
    dialect: "postgres",
    host: "localhost",
    port: 5432,
    database: "mjf",
    username: "postgres",
    password: "docker",
  }
}