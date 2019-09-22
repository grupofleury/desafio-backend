import { Request, Response } from 'express'
import { ScheduleService } from '../../services/index'

class ScheduleController {

    private service: ScheduleService

    public constructor () {
        this.service = new ScheduleService()
        this.schedule = this.schedule.bind(this)
        this.update = this.update.bind(this)
        this.get = this.get.bind(this)
        this.remove = this.remove.bind(this)
    }

    public async schedule(request: Request, response: Response): Promise<Response> {
        const result = await this.service.save(request.body)
        if(!result) {
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

    public async update(request: Request, response: Response): Promise<Response> {
        const result = await this.service.update(parseInt(request.params.id), request.body.date)
        if(!result) {
            response.status(409)
        }

        return response.send(result)
    }

    public async get(request: Request, response: Response): Promise<Response> {
        const result = await this.service.listByCpf(request.params.cpf)
        if (!result.schedules.length) {
            response.status(409)
        }
        return response.send({
            data: result.schedules.length ? result : null,
            success: result.schedules.length ? true : false,
            message: result.schedules.length ? 'data found successfully' : 'data search failed'
        })
    }

    public async remove(request: Request, response: Response): Promise<Response> {
        const result = await this.service.remove(parseInt(request.params.id))
        if (!result) {
            response.status(409)
        }
        return response.send(result)
    }
}

export { ScheduleController }