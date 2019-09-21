import express from 'express'
import swaggerUi from 'swagger-ui-express'
import routes from './routes/exam'
import * as swaggerDocument from '../swagger.json'

class App {
    private express: express.Application

    public constructor () {
        this.express = express()
        this.routes()
        this.swagger()
    }

    private routes (): void {
        this.express.use(routes)
    }

    private swagger (): void {
        this.express.use('/documentation', swaggerUi.serve, swaggerUi.setup(swaggerDocument))
    }

    public start(port: number): void {
        this.express.listen(port)
    }
}
export default new App()