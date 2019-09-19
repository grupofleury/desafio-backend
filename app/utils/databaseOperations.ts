import {createConnection} from "typeorm";
import { ClienteOperations } from "./clienteOperations";
import { AgendamentoOperations } from "./agendamentoOperations";

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

    async buscaCliente(body){
        let newClient = new ClienteOperations();
        return await newClient.buscaCliente(await this.connection,body);
    }

    async listaClientes(body){
        let newClient = new ClienteOperations();
        return await newClient.listaClientes(await this.connection,body);
    }
    
    async salvaAgendamento(body){
        let newAgendamentos = new AgendamentoOperations();
        return await newAgendamentos.saveAgendamentos(await this.connection,body.agendamentos,body.cpf);
    }

    async buscaAgendamento(body){
        let newAgendamentos = new AgendamentoOperations();
        return await newAgendamentos.buscaAgendamento(await this.connection,body);
    }

    async updateAgendamento(body){
        let newAgendamentos = new AgendamentoOperations();
        return await newAgendamentos.updateAgendamento(await this.connection,body);
    }

    async deleteAgendamento(body){
        let newAgendamentos = new AgendamentoOperations();
        return await newAgendamentos.deleteAgendamento(await this.connection,body);
    }
}


