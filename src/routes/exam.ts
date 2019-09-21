import { Router } from 'express'

const ExamRoutes = Router()
import  ExamController  from '../http/controllers/examController'

ExamRoutes.get('/exams', ExamController.list)

export { ExamRoutes }