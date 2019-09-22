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
        expect(examResult).toEqual(data)
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
})
