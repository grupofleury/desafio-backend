class DB {
    private static collection: DB
    private static customer: Array<any>
    private static schedule: Array<any>

    private constructor() {
        DB.customer = []
        DB.schedule = []
    }

    public static connection(): Object {
        if (!DB.collection) {
            DB.collection = new DB()
        }
        return DB.collection
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