import { Agendamento } from "../entities/agendamento";
import {validate} from "class-validator";
 

export class AgendamentoOperations{
    agendamentoOrm:any;

    async saveAgendamentos(connection,agendamentos,cpf){
        
        let arrRet = [];
        for(let ag of agendamentos){        
            let a;a=ag;
            this.agendamentoOrm = new Agendamento();
            this.agendamentoOrm.cpf = cpf;
            this.agendamentoOrm.name = a.name;
            this.agendamentoOrm.value = a.value;
            this.agendamentoOrm.data = a.data;
            this.agendamentoOrm.horario = a.horario;

            let err:any = new Promise((resolve)=>validate(this.agendamentoOrm).then(errors => {
                    resolve(errors);
            }))
            console.log('aqui')
            let errAwait = await err;

            switch(errAwait.length >0) { 
                case true : { 
                    console.log('aqui1')
                    arrRet.push(errAwait);
                   break; 
                } 
                default: { 
                    console.log('aqui2')
                    arrRet.push(this.agendamentoOrm);
                   break; 
                } 
            } 

        }
        return await arrRet;
    }

    async buscaAgendamento(connection,body){
        return await connection.getRepository(Agendamento)
        .createQueryBuilder("agendamento")
        .where("agendamento.cpf = :cpf", { cpf: body.cpf })
        .execute();
    }

    async updateAgendamento(connection,body){
        let jsonFiltro = {
            data:body.update.data,
            horario:body.update.horario
        }
        return await connection.createQueryBuilder()
        .update(Agendamento)
        .set(jsonFiltro)
        .where("id = :id", { id: body.id })
        .execute();
    }

    async deleteAgendamento(connection,body){
        console.log(body.id)
        return await connection.createQueryBuilder()
        .delete()
        .from(Agendamento)
        .where("id = :id", { id: body.id })
        .execute();
    }
}