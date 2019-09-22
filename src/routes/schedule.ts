import { Router } from 'express'

const ScheduleRoute = Router()
import  ScheduleController  from '../http/controllers/scheduleController'

ScheduleRoute.post('/schedules', ScheduleController.schedule)

export { ScheduleRoute }