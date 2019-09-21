import DB from '../../data/db'

class CustomerService {
    private connection: DB

    public constructor () {
        this.connection = DB.connection()
    }

    public save(data: any) {
        return this.connection.addCustomer(data)
    }

    public update(data: any): any {
        return this.connection.updateCustomer(data)
    }

    public remove(cpf: String): any {
        return this.connection.removeCustomer(cpf)
    }

    public find(cpf: String): any {
        return this.connection.findCustomer(cpf)
    }
    
    public list() {
        return this.connection.listCustomer()
    }
}

export default CustomerService