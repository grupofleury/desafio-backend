import { Exam } from '../models/exam'
import ExamProvider from '../providers/exams'

class ExamService {
    
    public list(): any {
        return ExamProvider.list()
    }

    public async get(id: string): Promise<Exam> {
        return await ExamProvider.byId(id)
    }
}

export { ExamService }