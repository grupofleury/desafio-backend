import ExamProvider from '../providers/exams'

class ExamsService {
    
    public list() {
        return ExamProvider.list()
    }

    public get(id: Number) {
        const exams = this.list()
    }
}

export default ExamsService