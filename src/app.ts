import express from 'express'
import swaggerUi from 'swagger-ui-express'
import { CustomerRoutes, ExamRoutes } from './routes/index'
import * as swaggerDocument from '../swagger.json'

class App {
    private express: express.Application

    public constructor () {
        this.express = express()
        this.middlwares()
        this.routes()
        this.swagger()
    }

    private routes (): void {
        this.express.use(CustomerRoutes)
        this.express.use(ExamRoutes)
    }
    
    private middlwares (): void {
        this.express.use(express.json())
    }

    private swagger (): void {
        this.express.use('/documentation', swaggerUi.serve, swaggerUi.setup(swaggerDocument))
    }

    public start(port: number): void {
        this.express.listen(port)
    }
}
export default new App()