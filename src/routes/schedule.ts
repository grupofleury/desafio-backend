import { Router } from 'express'

const ScheduleRoute = Router()
import  ScheduleController  from '../http/controllers/scheduleController'

ScheduleRoute.post('/schedules', ScheduleController.schedule)
ScheduleRoute.get('/schedules/:cpf', ScheduleController.get)

export { ScheduleRoute }