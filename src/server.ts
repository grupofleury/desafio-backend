import express from 'express'

const server: express.Application = express()

server.get('/', (request, response) => {
    response.send({ message: 'Welcome to my challenge'})
})

server.listen(4446, () => {
    console.log(`Server is running! Welcome!`)
})