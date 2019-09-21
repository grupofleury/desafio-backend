import axios from 'axios'
import ApiExamsService from '../../../src/services/apiExams'
import database from '../../../data/db'

jest.mock('axios')
const mockedAxios = axios as jest.Mocked<typeof axios>

test('should return exam list', () => {
    const exams = [
        { id: 1, name: 'Raio-x', value: 30.44 },
        { id: 2, name: 'Ultrassonografia', value: 46.21 },
        { id: 3, name: 'Endoscopia', value: 44.31 }
    ]
    
    const expected = [
        { id: 1, name: 'Raio-x' },
        { id: 2, name: 'Ultrassonografia' },
        { id: 3, name: 'Endoscopia' }
    ]

    mockedAxios.get.mockResolvedValue({ data: exams })

    return ApiExamsService.list().then( data => expect(data).toEqual(expected))

})

test('should save a client to the database', () => {
    const connection = database.connection()
    const customer = {
        name: 'Luiz Filho',
        cpf: '383383383-44'
    }

    const customerStored = connection.addCustomer(customer)
    const otherConnection = database.connection()
    const searched = otherConnection.getCustomer(customer.cpf)
    expect(customer).toEqual(searched)
})
