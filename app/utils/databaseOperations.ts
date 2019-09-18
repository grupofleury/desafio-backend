import {createConnection} from "typeorm";
import { ClienteOperations } from "./clienteOperations";

export class DatabaseOperations{
    connection: any

    constructor(){
        this.connection = new Promise((resolve,reject)=>{
            createConnection({
                "type": "mysql",
                "host": "remotemysql.com",
                "port": 3306,
                "username": "oX6IanxcSK",
                "password": "GrwwyDGqu5",
                "database": "oX6IanxcSK",
                "synchronize": true,
                "logging": false,
                "entities": [
                   "app/entities/**/*.ts"
                ]
             }).then(connection => { 
                resolve(connection);
            }).catch(error => reject(error));
        })
    }


    async saveCliente(cliente){
        let newClient = new ClienteOperations();
        return await newClient.saveCliente( await this.connection,cliente);
    }
    
    async updateCliente(body){
        let newClient = new ClienteOperations();
        return await newClient.updateCliente(await this.connection,body);
    }

    async deleteCliente(body){
        let newClient = new ClienteOperations();
        return await newClient.deleteCliente(await this.connection,body);
    }
}


