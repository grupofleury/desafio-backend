import {Calls} from './utils/calls'
import { DatabaseOperations } from './utils/databaseOperations';
const call:any = new Calls()
const database:any = new DatabaseOperations();

export class Tests{

    async testaListaExame(){
        let response = await call.getListExams();
        return await response
    }

    async testaNovoCliente(){
        let json ={
            "nome": "string",
            "idade": 0,
            "email": "string",
            "cpf": "string",
            "genero": true,
            "endereco": "string",
            "cidade": "string"
          }
        let response = await database.saveCliente(json);
        return  response
    }

    async testaUpdateCliente(){
        let json ={
            "id": 0,
            "update": {
              "nome": "string",
              "idade": 0,
              "email": "string",
              "cpf": "string",
              "genero": true,
              "endereco": "string",
              "cidade": "string"
            }
          }

        let response = await database.updateCliente(json);
        return response
    }


    async deletaCliente(){
        let json ={
            "id": 0
          }
        let response = await database.deleteCliente(json);
        return response
    }

    async findCliente(){
        let json ={
            "cpf": "string"
          }
        let response = await database.buscaCliente(json);
        return response
    }

    async listaCliente(){
        let json ={
            "count_since_record": 0
          }
        let response = await database.listaClientes(json);
        return response
    }

    async findAgendamentos(){
        let json ={
            "cpf": "string"
          }
        let response = await database.buscaAgendamento(json);
        return response
    }


    async updateAgendamento(){
        let json ={
            "id": 0,
            "update": {
              "data": "2019-09-20",
              "horario": "2019-09-20T03:22:03.704Z"
            }
          }
        let response = await database.updateAgendamento(json);
        return response
    }

    async deleteAgendamento(){
        let json ={
            "id": 0,
            "update": {
              "data": "2019-09-20",
              "horario": "2019-09-20T03:22:03.704Z"
            }
          }
        let response = await database.deleteAgendamento(json);
        return response
    }

    async agendamentoCliente(){
        let json ={
            "cpf": "string",
            "agendamentos": {
              "idAgendamento": "string",
              "name": "string",
              "value": 0,
              "data": "2019-09-20",
              "horario": "2019-09-20T03:23:10.589Z"
            }
          }
        let response = await database.salvaAgendamento(json);
        return response
    }
}