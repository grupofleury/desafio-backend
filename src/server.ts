import express from 'express'
import swaggerUi from 'swagger-ui-express'
import * as swaggerDocumentat from '../swagger.json'

const server: express.Application = express()

server.get('/', (request, response) => {
    response.send({ message: 'Welcome to my challenge'})
})

server.use('/swagger', swaggerUi.serve, swaggerUi.setup(swaggerDocumentat))

server.listen(4446, () => {
    console.log(`Server is running! Welcome!`)
})