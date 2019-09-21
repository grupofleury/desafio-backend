import DB from '../../data/db'

class CustomerService {
    private connection: DB

    public constructor () {
        this.connection = DB.connection()
    }

    public save(data: any) {
        return this.connection.addCustomer(data)
    }
}

export default CustomerService