import request from 'request-promise-native'
import ExamProvider from '../../../src/providers/exams'

jest.mock('request-promise-native')
const mockedAxios = request as jest.Mocked<typeof request>

test('should return exam list', async () => {
    const exams = '{"exams": [{"id":"1","name":"17 soro","value":35.60},{"id":"2","name":"Acidificação Urinária","value":84.90},{"id":"3","name":"Ácido Ascórbico, plasma","value":99.90}]}'
    
    const expected = [
        { id: "1", name: '17 soro' },
        { id: "2", name: 'Acidificação Urinária' },
        { id: "3", name: 'Ácido Ascórbico, plasma' }
    ]

    mockedAxios.get.mockResolvedValue(exams)
    let response = await ExamProvider.list()
    expect(response).toEqual(expected)
})

