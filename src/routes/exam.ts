import { Router } from 'express'

const ExamRoutes = Router()
import  ExamController  from '../http/controllers/examController'

ExamRoutes.get('/exams', ExamController.list)
ExamRoutes.get('/exams/:id', ExamController.get)

export { ExamRoutes }