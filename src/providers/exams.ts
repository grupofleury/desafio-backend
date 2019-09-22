import request from 'request-promise-native'
import formatter from '../helpers/formatter'
import { Exam } from '../models/exam'

class ExamProvider {
    static async list(): Promise<Exam[]> {        
        let exams =  await request.get('http://www.mocky.io/v2/5d681ede33000054e7e65c3f')
        return formatter.extractFields(JSON.parse(exams.toString()).exams)
    }

    static async byId(id: String): Promise<any> {        
        let exams =  await request.get('http://www.mocky.io/v2/5d681ede33000054e7e65c3f')
        let formatted =  JSON.parse(exams.toString()).exams
        return formatted.find( (item: any) => item.id === id)
    }
}

export default ExamProvider