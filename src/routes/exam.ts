import { Router } from 'express'

const routes = Router()
import  ExamController  from '../http/controllers/examController'

routes.get('/exams', ExamController.list)

export default routes