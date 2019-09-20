import axios from 'axios'
import ApiExamsService from '../../../src/services/apiExams'

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
