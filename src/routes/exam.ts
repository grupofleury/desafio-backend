import { Router } from 'express'

const ExamRoutes = Router()
import  { ExamController }  from '../http/controllers/examController'
const examController = new ExamController()

ExamRoutes.get('/exams', examController.list)
ExamRoutes.get('/exams/:id', examController.get)

export { ExamRoutes }