import DB from '../../data/db'
import { ExamService } from './index'
import { CustomerService } from './index'
import { Schedule } from '../models/schedule'

class ScheduleService {
    private connection: DB
    private exam: ExamService
    private customer: CustomerService

    public constructor () {

        this.connection = DB.connection()
        this.exam = new ExamService()
        this.customer = new CustomerService()
    }

    public async save(data: Schedule) {

        const exam = await this.exam.get(data.examId.toString())
        const customer = await this.customer.find(data.cpf)

        if (exam && customer.success) {

            let formattedDate = data.date

            const customersAtThisTime = this.connection.getByScheduleByDate(data.examId.toString(), formattedDate)
            const maxByTime = process.env.MAX_BY_TIME || 2

            if (customersAtThisTime.length < maxByTime) {
                data.price = exam.value
                formattedDate = data.date
                data = { ...data, date: formattedDate }
                return this.connection.createSchedule({ ...data, examId: data.examId.toString() })
            }
        }

        return null
    }

    public listByCpf(cpf: string) {

        let schedules: any = this.connection.getScheduleByCpf(cpf)
        let pricesSum = 0

        if (schedules) {
            pricesSum = schedules.reduce( (sum: number, currentPrice: any) => sum + currentPrice.price, 0)
            schedules = { schedules, total: pricesSum }
        }

        return schedules
    }

    public async update(id: number, date: any) {
        const formattedDate = date
        return this.connection.updateSchedule(id, formattedDate)
    }

    public async remove(id: number) {
        return this.connection.removeSchedule(id)
    }
}

export { ScheduleService }