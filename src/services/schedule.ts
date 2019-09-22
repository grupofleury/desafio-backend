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
        let exam = await this.exam.get(data.examId.toString())
        let customer = await this.customer.find(data.cpf)
        if (exam && customer.success) {
            let formattedDate = moment(data.date).format()
            const alreadyScheduled = this.connection.getByScheduleByDate(data.examId.toString(), formattedDate)
            if (!alreadyScheduled) {
                data.price = exam.value
                data = { ...data, date: moment(data.date).format() }
                return this.connection.schedule({ ...data, examId: data.examId.toString() })
            }
        }
        return null
    }

    public listByCpf(cpf: String) {
        const result = this.connection.getScheduleByCpf(cpf)
        let pricesSum = 0
        if (!!result) {
            pricesSum = result.reduce( (sum, currentPrice) => sum + currentPrice.price, 0)
        }
        return { ...result, total: pricesSum }
    }

}

export default ScheduleService