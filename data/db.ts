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
        DB.customer.push(data)
        return DB.customer.find( item => item.cpf === data.cpf)
    }

    public addSchedule(data: any) {
        DB.schedule.push(data)
        return DB.schedule.find( item => item.cpf == data.cpf )
    }
}

export default DB