class DB {
    private static instance: DB
    private static customer: Array<any>
    private static schedule: Array<any>

    private constructor() {
        DB.customer = []
        DB.schedule = []
    }

    public static connection(): DB {
        if (!DB.instance) {
            DB.instance =  new DB()
        }
        return DB.instance
    }

    public findCustomer(cpf: String) {
        const customer =  DB.customer.find( item => item.cpf === cpf)
        return {
            success: customer ? true: false,
            data: customer || null,
            message: customer ? 'item successfully find' : 'item not found'
        }
    }

    public addCustomer(data: any) {

        let result = !DB.customer.find(item => item.cpf === data.cpf) ? DB.customer.push(data) : null

        return {
            success: result ? true: false,
            data: DB.customer.find( item => item.cpf === data.cpf && item.name === data.name) || null,
            message: result ? 'item successfully saved' : 'item already exists'
        }
    }

    public updateCustomer(data: any) {
        let customerIndex =  DB.customer.findIndex( item => item.cpf === data.cpf )
        if (customerIndex > -1) {
            DB.customer[customerIndex] = data
        }

        return {
            success: customerIndex > -1 ? true: false,
            data: DB.customer.find( item => item.cpf === data.cpf ) || null,
            message: customerIndex > -1 ? 'item successfully updated' : 'item not found'
        }
    }

    public removeCustomer(cpf: String): any {
        let customer =  DB.customer.find( item => item.cpf === cpf )

        if (customer) {
            DB.customer = DB.customer.filter( item => item.cpf !== cpf)
        }

        return {
            success: customer ? true: false,
            data: customer || null,
            message: customer ? 'item successfully removed' : 'item not found'
        }
    }
}

export default DB