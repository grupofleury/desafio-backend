import * as express from 'express'
import * as bodyParser from 'body-parser'

import ClientController from '../controllers/client-controller'
import ScheduleController from '../controllers/schedule-controller'
import ClientValidator from '../validators/client-validator'

class Server {
  httpServer: any
  port: number
  app: express.Express = null

  constructor (port) {
    this.port = port || 80
    this.app = express()
  }

  listen (): void {
    this.httpServer = this.app.listen(this.port, () => {
      console.log(`Server listening on 0.0.0.0:${this.port}`)
    })
  }

  close (): void {
    this.httpServer.close()
  }

  middlewares (): void {
    this.app.use(bodyParser.json())
    this.app.use(bodyParser.urlencoded({ extended: true }))
  }

  routes (): void {
    this.app.get('/', (req, res) => {
      res.json({ message: 'Welcome Schedule API' })
    })
    this.app.get('/clients', ClientController.findAll)
    this.app.get('/clients/cpf/:cpf', ClientController.findByCPF)
    this.app.get('/clients/cpf/:cpf/schedules', ScheduleController.findByCPF)
    this.app.post('/clients', ClientValidator.createValidationBody(), ClientController.create)
    this.app.put('/clients/:id', ClientValidator.updateValidationBody(), ClientController.update)
    this.app.delete('/clients/:id', ClientController.delete)
    this.app.post('/schedules', ScheduleController.create)
    this.app.put('/schedules/:id', ScheduleController.update)
    this.app.delete('/schedules/:id', ScheduleController.delete)
  }

  bootstrap (): void {
    this.middlewares()
    this.routes()
    this.listen()
  }
}

export default new Server(parseInt(process.env.SCHEDULE_API_PORT))
