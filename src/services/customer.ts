import DB from '../../data/db'

class CustomerService {
    private connection: DB

    public constructor () {
        this.connection = DB.connection()
    }

    public save(data: any) {
        return this.connection.addCustomer(data)
    }

    public update(data: {cpf: String, name: String}): any {
        return this.connection.updateCustomer(data)
    }
}

export default CustomerService