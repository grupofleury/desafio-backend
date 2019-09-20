import { Agendamento } from "../entities/agendamento";
import {validate} from "class-validator";
import { Cliente } from "../entities/cliente";


export class AgendamentoOperations{
    agendamentoOrm:any;

    async saveAgendamentos(connection,agendamentos,bodycpf){
        let arrRet = [];    
        
            this.agendamentoOrm = new Agendamento();
            this.agendamentoOrm.cpf = bodycpf;
            this.agendamentoOrm.name = agendamentos.name;
            this.agendamentoOrm.value = agendamentos.value;
            this.agendamentoOrm.data = agendamentos.data;
            this.agendamentoOrm.horario = agendamentos.horario;
            this.agendamentoOrm.idAgendamento = agendamentos.idAgendamento;
            let err:any = new Promise((resolve)=>validate(this.agendamentoOrm).then(errors => {
                    resolve(errors);
            }))
            let errAwait = await err;
            switch(errAwait.length >0) { 
                case true : { 
                    arrRet.push(errAwait);
                   break; 
                } 
                default: { 
                    arrRet.push(await connection.manager.save(this.agendamentoOrm));
                   break; 
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
        return await connection.createQueryBuilder()
        .delete()
        .from(Agendamento)
        .where("id = :id", { id: body.id })
        .execute();
    }

    async alreadyExistAgendamento(connection,body){
        console.log(body)
        return await connection.getRepository(Agendamento)
        .createQueryBuilder("agendamento")
        .where("agendamento.idAgendamento = :idAgendamento", { idAgendamento: body.agendamentos.idAgendamento })
        .andWhere("agendamento.horario = :horario", { horario: body.agendamentos.horario })
        .andWhere("agendamento.data = :data", { data: body.agendamentos.data })
        .getMany();
    }
}