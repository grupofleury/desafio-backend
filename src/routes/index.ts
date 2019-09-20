import { Router } from 'express'

const routes = Router()

routes.get('/', (request, response) => {
    response.send({ message: 'Welcome' })
})

export default routes