import { Cliente } from "../entities/cliente";

export class ClienteOperations{
    clienteOrm: Cliente
    constructor(cliente){
        this.clienteOrm = new Cliente();
        this.clienteOrm.nome = cliente.nome;
        this.clienteOrm.idade = cliente.idade;
        this.clienteOrm.email = cliente.email;
        this.clienteOrm.cpf = cliente.cpf;
        this.clienteOrm.genero = cliente.genero;
        this.clienteOrm.endereco = cliente.endereco;
        this.clienteOrm.cidade = cliente.cidade;
    }

    async saveCliente(connection){
        return await connection.manager.save(this.clienteOrm)
    }
}