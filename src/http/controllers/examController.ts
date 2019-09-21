import { Request, Response } from 'express'
import apiExam from '../../services/apiExams'
import formatter from '../../helpers/formatter'

class ExamController {
    public async list(request: Request, response: Response): Promise<Response> {
        const exams = await apiExam.list()
        const responseFormatted = formatter.extractFields(JSON.parse(exams.toString()).exams)
        return response.send(responseFormatted)
    }
}

export default new ExamController()