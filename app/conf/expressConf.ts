import * as express from 'express'
import * as bodyParser  from 'body-parser'
const swaggerUi = require('swagger-ui-express');
const swaggerDocument = require('../../swagger.json');

export class ExpressConf{


    constructor(){
        const app:any = express();
        app.use('/api-docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));
        app.use(    bodyParser.json() );       // to support JSON-encoded bodies
        app.use(    bodyParser.urlencoded({     // to support URL-encoded bodies
        extended: true
        }));
        app.use(express.json());       // to support JSON-encoded bodies
        app.use(express.urlencoded()); // to support URL-encoded bodies
        return app;
    }
}