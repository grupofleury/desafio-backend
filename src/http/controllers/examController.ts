import { Request, Response } from 'express'
import { ExamService } from '../../services/index'

class ExamController {

    private service: ExamService

    public constructor () {
        this.service = new ExamService()
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

export { ExamController }