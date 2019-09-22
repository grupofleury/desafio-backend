import { Exam } from '../models/exam'
import ExamProvider from '../providers/exams'

class ExamsService {
    
    public list(): any {
        return ExamProvider.list()
    }

    public async get(id: String): Promise<Exam> {
        return await ExamProvider.byId(id)
    }
}

export default ExamsService