import request from 'request-promise-native'
import DB from '../../../data/db'
import { fullName, getCpf, getDate, getFutureDate, getExamsMock } from '../utils/dataGenerate'
import ScheduleService from '../../../src/services/schedule'
import moment from 'moment'

jest.mock('request-promise-native')
const mockedAxios = request as jest.Mocked<typeof request>

let customer = {
    cpf: '',
    name: '',
    dateOfBirth: new Date()
}

let scheduleService = new ScheduleService()
const examsMock = getExamsMock()

describe('Scheduling exam', () => {

    beforeEach( async () => {

        DB.resetForUnitTest()

        let customerData = {
            name: fullName(),
            cpf: getCpf(),
            dateOfBirth: getDate()
        }
        let connection = DB.connection()
        customer = connection.addCustomer(customerData).data
        mockedAxios.get.mockResolvedValue(examsMock)
    })
    
    it('should be scheduled an exam for a patient enrolled in the database', async () => {
        const data = {
            examId: (Math.floor(Math.random() * 3) + 1).toString(),
            cpf: customer.cpf,
            date: moment(getFutureDate()).format()
        }
        mockedAxios.get.mockResolvedValue(examsMock)
        let examResult = await scheduleService.save(data)
        const {  id, ...received } = examResult
        expect(received).toEqual(data)
    })

    it('should not be scheduled an exam  for the wrong exam id', async () => {
        const data = {
            examId: '11',
            cpf: customer.cpf,
            date: getFutureDate()
        }
        mockedAxios.get.mockResolvedValue(examsMock)
        let examResult = await scheduleService.save(data)
        expect(examResult).toEqual(null)
    })

    it('should not schedule a busy time', async () => {
        const data = {
            examId: (Math.floor(Math.random() * 3) + 1).toString(),
            cpf: customer.cpf,
            date: moment(getFutureDate()).format()
        }
        mockedAxios.get.mockResolvedValue(examsMock)
        await scheduleService.save(data)
        let connectionToSameTime = DB.connection()

        let otherCustomer = connectionToSameTime.addCustomer({ ...customer, cpf: getCpf() }).data

        let otherExamSameTime = await scheduleService.save({ ...data, cpf: otherCustomer.cpf })
        expect(otherExamSameTime).toEqual(null)
    })

    it('should return exams by cpf and calculate the total value', async () => {

        mockedAxios.get.mockResolvedValue(examsMock)
        let sum = 0
        for(let index = 0; index < 10; index++) {

            const data = {
                examId: (Math.floor(Math.random() * 3) + 1).toString(),
                cpf: customer.cpf,
                date: moment(getFutureDate()).format()
            }
            let saved = await scheduleService.save(data)
            sum += saved.price
        }
        let scheduleByCpf = scheduleService.listByCpf(customer.cpf)
        expect(scheduleByCpf.total).toEqual(sum)
    })

    it('should edit a schedule by id', async () => {
        const data = {
            examId: (Math.floor(Math.random() * 3) + 1).toString(),
            cpf: customer.cpf,
            date: moment(getFutureDate()).format()
        }
        mockedAxios.get.mockResolvedValue(examsMock)
        let examResult = await scheduleService.save(data)
        const {  id } = examResult

        const newDate = moment(getFutureDate()).format()

        let updatedSchedule = await scheduleService.update(id, newDate)

        expect(updatedSchedule.data.date).toEqual(newDate)
    })

    it('should remove a schedue by id', async () => {
        const data = {
            examId: (Math.floor(Math.random() * 3) + 1).toString(),
            cpf: customer.cpf,
            date: moment(getFutureDate()).format()
        }
        mockedAxios.get.mockResolvedValue(examsMock)
        let examResult = await scheduleService.save(data)
        const {  id } = examResult

        let removed = await scheduleService.remove(id)

        expect(removed.data).toEqual(examResult)
    })
})
