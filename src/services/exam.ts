import ExamProvider from '../providers/exams'

class ExamsService {
    
    public list() {
        return ExamProvider.list()
    }

    public async get(id: any): Promise<any> {
        const exams = await this.list()
        return exams.find( (item: any) => item.id === id)
    }
}

export default ExamsService