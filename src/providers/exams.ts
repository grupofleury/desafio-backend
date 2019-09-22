import request from 'request-promise-native'
import formatter from '../helpers/formatter'

class ExamProvider {
    static async list(): Promise<any> {        
        let exams =  await request.get('http://www.mocky.io/v2/5d681ede33000054e7e65c3f')
        return formatter.extractFields(JSON.parse(exams.toString()).exams)
    }
}

export default ExamProvider