import ExamProvider from '../providers/exams'

class ExamsService {
    
    public list() {
        return ExamProvider.list()
    }

    public async get(id: any): Promise<any> {
        return await ExamProvider.byId(id)
    }
}

export default ExamsService