import * as express from 'express'
import * as bodyParser  from 'body-parser'

export class ExpressConf{


    constructor(){
        const app:any = express()
        app.use(    bodyParser.json() );       // to support JSON-encoded bodies
        app.use(    bodyParser.urlencoded({     // to support URL-encoded bodies
        extended: true
        }));
        app.use(express.json());       // to support JSON-encoded bodies
        app.use(express.urlencoded()); // to support URL-encoded bodies
        return app;
    }
}