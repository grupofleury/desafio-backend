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

    public getCustomer(cpf: String) {
        return DB.customer.find( item => item.cpf === cpf)
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

    public addSchedule(data: any) {
        DB.schedule.push(data)
        return DB.schedule.find( item => item.cpf == data.cpf )
    }
}

export default DB