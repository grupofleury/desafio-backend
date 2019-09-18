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
        console.log(body.id)
        return await connection.createQueryBuilder()
        .delete()
        .from(Cliente)
        .where("id = :id", { id: body.id })
        .execute();
    }
}