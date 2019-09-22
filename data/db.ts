class DB {
    private static instance: DB
    private static customers: Array<any>
    private static schedules: Array<any>

    private constructor() {
        DB.customers = []
        DB.schedules = []
    }

    public static connection(): DB {
        if (!DB.instance) {
            DB.instance =  new DB()
        }
        return DB.instance
    }

    public findCustomer(cpf: String) {
        const customer =  DB.customers.find( item => item.cpf === cpf)
        return {
            success: customer ? true: false,
            data: customer || null,
            message: customer ? 'item successfully find' : 'item not found'
        }
    }

    public addCustomer(data: any) {

        let result = !DB.customers.find(item => item.cpf === data.cpf) ? DB.customers.push({...data}) : null

        return {
            success: result ? true: false,
            data: DB.customers.find( item => item.cpf === data.cpf && item.name === data.name) || null,
            message: result ? 'item successfully saved' : 'item already exists'
        }
    }

    public updateCustomer(data: any) {
        let customerIndex =  DB.customers.findIndex( item => item.cpf === data.cpf )
        if (customerIndex > -1) {
            DB.customers[customerIndex] = { ...data }
        }

        return {
            success: customerIndex > -1 ? true: false,
            data: DB.customers.find( item => item.cpf === data.cpf ) || null,
            message: customerIndex > -1 ? 'item successfully updated' : 'item not found'
        }
    }

    public removeCustomer(cpf: String): any {
        let customer =  DB.customers.find( item => item.cpf === cpf )

        if (customer) {
            DB.customers = DB.customers.filter( item => item.cpf !== cpf)
        }

        return {
            success: customer ? true: false,
            data: customer || null,
            message: customer ? 'item successfully removed' : 'item not found'
        }
    }

    public listCustomer() {
        const customers = DB.customers
        return {
            success: customers ? true: false,
            data: customers || null,
            message: customers ? 'items successfully removed' : 'items not found'
        }
    }

    public static resetForUnitTest() {
        DB.instance = new DB()
    }

    public schedule(data: any): any {
        let id = DB.schedules.length + 1
        DB.schedules.push({ ...data, id })
        return DB.schedules.find( item => item.cpf == data.cpf && item.examId === data.examId)
    }

    public updateSchedule(id: any, date: any): any {
        let scheduleIndex =  DB.schedules.findIndex( item => item.id === id )
        if (scheduleIndex > -1) {
            DB.schedules[scheduleIndex] = { ...DB.schedules[scheduleIndex] , date: date}
        }

        return {
            success: scheduleIndex > -1 ? true: false,
            data: DB.schedules.find( item => item.id === id ) || null,
            message: scheduleIndex > -1 ? 'item successfully updated' : 'item not found'
        }
    }

    public getByScheduleByDate( examId: String, date: any ) {
        return !!DB.schedules.find( item => item.examId === examId && item.date === date )
    }

    public getScheduleByCpf(cpf: String) {
        return DB.schedules.filter(item => item.cpf === cpf)
    }
 }

export default DB