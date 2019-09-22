import DB from '../../data/db'
import { Customer } from '../models/customer'

class CustomerService {
    private connection: DB

    public constructor () {
        this.connection = DB.connection()
    }

    public save(data: Customer) {
        return this.connection.addCustomer(data)
    }

    public update(data: Customer): any {
        return this.connection.updateCustomer(data)
    }

    public remove(cpf: String): any {
        return this.connection.removeCustomer(cpf)
    }

    public find(cpf: String): any {
        return this.connection.findCustomer(cpf)
    }
    
    public list(): any {
        return this.connection.listCustomer()
    }
}

export default CustomerService