import { Request, Response } from 'express'
import ExamsService from '../../services/exam'

class ExamController {
    
    private service: ExamsService

    public constructor () {
        this.service = new ExamsService()
        this.list = this.list.bind(this)
    }
    public async list(request: Request, response: Response): Promise<Response> {
        const exams = await this.service.list()
        return response.send(exams)
    }
}

export default new ExamController()