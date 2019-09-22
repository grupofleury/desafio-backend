import { Router } from 'express'

const ScheduleRoute = Router()
import  ScheduleController  from '../http/controllers/scheduleController'

ScheduleRoute.post('/schedules', ScheduleController.schedule)
ScheduleRoute.put('/schedules/:id', ScheduleController.update)
ScheduleRoute.get('/schedules/:cpf', ScheduleController.get)

export { ScheduleRoute }