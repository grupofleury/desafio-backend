import { Router } from 'express'

const ScheduleRoute = Router()
import  { ScheduleController }  from '../http/controllers/scheduleController'
const scheduleController = new ScheduleController

ScheduleRoute.post('/schedules', scheduleController.schedule)
ScheduleRoute.put('/schedules/:id', scheduleController.update)
ScheduleRoute.get('/schedules/:cpf', scheduleController.get)
ScheduleRoute.delete('/schedules/:id', scheduleController.remove)

export { ScheduleRoute }