const swaggerUi = require('swagger-ui-express');
const swaggerJsdoc = require('swagger-jsdoc');

const options = {
  swaggerDefinition: {
    // Like the one described here: https://swagger.io/specification/#infoObject
    info: {
      title: 'Grupo Fleury',
      version: '1.0.0',
      description: 'API para teste do Grupo Fleury',
    },
  },
  // List of files to be processes. You can also set globs './routes/*.js'
  apis: ["./routes/*.js"],
};

const specs = swaggerJsdoc(options);

module.exports = (app) => {
    app.use('/swagger', swaggerUi.serve, swaggerUi.setup(specs));
}