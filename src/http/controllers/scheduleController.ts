import { Request, Response } from 'express'
import ScheduleService from '../../services/schedule'

class ScheduleController {

    private service: ScheduleService

    public constructor () {
        this.service = new ScheduleService()
        this.schedule = this.schedule.bind(this)
    }

    public async schedule(request: Request, response: Response): Promise<Response> {
        const result = await this.service.save(request.body)
        if(!!result) {
            response.status(409)
        }

        return response.send(
            {
                data: result,
                success: !!result ? true : false,
                message: !!result ? 'Successful Scheduling' : 'Schedule failed'
            }
        )
    }
}

export default new ScheduleController()