import DB from '../../data/db'
import ExamService from './exam'
import CustomerService from './customer'
import moment from 'moment'

class ScheduleService {
    private connection: DB
    private exam: ExamService
    private customer: CustomerService

    public constructor () {
        this.connection = DB.connection()
        this.exam = new ExamService()
        this.customer = new CustomerService()
    }

    public async save(data: any) {
        let exam = await this.exam.get(data.examId)
        let customer = await this.customer.find(data.cpf)
        if (exam && customer.success) {
            const alreadyScheduled = this.connection.getByScheduleByDate(data.examId, data.date)
            if (!alreadyScheduled) {
                data = { ...data, date: moment(data.date).format() }
                return this.connection.schedule(data)
            }
        }
        return null
    }

}

export default ScheduleService