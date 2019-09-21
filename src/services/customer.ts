import DB from '../../data/db'

class CustomerService {
    private connection: DB

    public constructor () {
        this.connection = DB.connection()
    }

    public save(data:Object) {
        return this.connection.addCustomer(data)
    }
}

export default CustomerService