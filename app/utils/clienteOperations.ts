import { Cliente } from "../entities/cliente";

export class ClienteOperations{
    clienteOrm: Cliente
    
    async saveCliente(connection,cliente){
        this.clienteOrm = new Cliente();
        this.clienteOrm.nome = cliente.nome;
        this.clienteOrm.idade = cliente.idade;
        this.clienteOrm.email = cliente.email;
        this.clienteOrm.cpf = cliente.cpf;
        this.clienteOrm.genero = cliente.genero;
        this.clienteOrm.endereco = cliente.endereco;
        this.clienteOrm.cidade = cliente.cidade;
        return await connection.manager.save(this.clienteOrm)
    }

    async updateCliente(connection,body){
        return await connection.createQueryBuilder()
        .update(Cliente)
        .set(body.update)
        .where("id = :id", { id: body.id })
        .execute();
    }

    async deleteCliente(connection,body){
        return await connection.createQueryBuilder()
        .delete()
        .from(Cliente)
        .where("id = :id", { id: body.id })
        .execute();
    }

    async buscaCliente(connection,body){
         return await connection.getRepository(Cliente)
        .createQueryBuilder("cliente")
        .where("cliente.cpf = :cpf", { cpf: body.cpf })
        .getOne();
    }

    async qtdCliente(connection,body){
        return await connection.getRepository(Cliente)
       .createQueryBuilder("cliente")
       .where("cliente.cpf = :cpf", { cpf: body.cpf })
       .execute();
   }

    async listaClientes(connection,body){
        let total = await connection.getRepository(Cliente).createQueryBuilder().getCount();
        let resPag = await connection.getRepository(Cliente)
        .find({ 
            skip: 0, 
            take: 10 
        });
        return {
            total,
            resPag
        }
   }

}