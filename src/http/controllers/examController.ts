import { Request, Response } from 'express'
import ExamsService from '../../services/exam'

class ExamController {

    private service: ExamsService

    public constructor () {
        this.service = new ExamsService()
        this.list = this.list.bind(this)
        this.get = this.get.bind(this)
    }

    public async list(request: Request, response: Response): Promise<Response> {
        const exams = await this.service.list()
        return response.send(exams)
    }

    public async get(request: Request, response: Response): Promise<Response> {
        const exam = await this.service.get(request.params.id)
        return response.send(exam)
    }
}

export default new ExamController()