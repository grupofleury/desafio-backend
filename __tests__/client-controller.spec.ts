import * as fs from 'fs'
import { createConnection, ConnectionOptions } from 'typeorm'
import * as request from 'supertest'
import Server from '../src/server'

import Client from '../src/entities/client'
import Exam from '../src/entities/exam'
import Schedule from '../src/entities/schedule'

describe('Make test of client controller', () => {
  let connection

  beforeAll(async (done) => {
    const options: ConnectionOptions = {
      type: 'sqlite',
      database: '__tests__/database-client-test.db',
      entities: [Exam, Schedule, Client],
      migrationsRun: true,
      migrations: ['migrations/*.ts']
    }

    connection = await createConnection(options)
    Server.bootstrap()
    done()
  })

  afterAll(async (done) => {
    try {
      await Server.close()
      await connection.close()
      fs.unlinkSync('__tests__/database-client-test.db')
      done()
    } catch (error) {
      console.log('error', error)
      done(error)
    }
  })

  it('Should create client with success', async (done) => {
    try {
      const bodyToCreate = {
        cpf: '44846138836',
        name: 'Jeff',
        birthDate: '1996-04-02T00:00:00-03:00'
      }

      const result = await request(Server.app)
        .post('/clients')
        .send(bodyToCreate)
        .expect(201)

      expect(result.body).not.toBe(null)
      const { body } = result

      expect(body.cpf).toEqual(bodyToCreate.cpf)
      expect(body.name).toEqual(bodyToCreate.name)
      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should not create client, cpf is invalid', async (done) => {
    try {
      const bodyToCreate = {
        cpf: '44846138832',
        name: 'Jeff',
        birthDate: '1996-04-02T00:00:00-03:00'
      }
      const result = await request(Server.app)
        .post('/clients')
        .send(bodyToCreate)
        .expect(400)

      expect(result.body).not.toBe(null)
      const { body } = result

      expect(body.errorMessage).toEqual('CPF is invalid')

      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should not create client, client duplicated', async (done) => {
    try {
      const bodyToCreate = {
        cpf: '44846138836',
        name: 'Jeff',
        birthDate: '1996-04-02T00:00:00-03:00'
      }
      const result = await request(Server.app)
        .post('/clients')
        .send(bodyToCreate)
        .expect(422)

      expect(result.body).not.toBe(null)
      const { body } = result

      expect(body.errorMessage).toEqual('CPF duplicated on database')

      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should list client with success', async (done) => {
    try {
      const result = await request(Server.app)
        .get('/clients')
        .expect(200)

      expect(result.body).not.toBe(null)
      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should find one client by cpf with success', async (done) => {
    try {
      const cpf = '44846138836'

      const result = await request(Server.app)
        .get(`/clients/cpf/${cpf}`)
        .expect(200)

      expect(result.body).not.toBe(null)
      const { body } = result

      expect(body.cpf).toEqual(cpf)
      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should not find one client, cpf is invalid', async (done) => {
    try {
      const cpf = '44846138832'

      const result = await request(Server.app)
        .get(`/clients/cpf/${cpf}`)
        .expect(400)

      expect(result.body).not.toBe(null)
      const { body } = result

      expect(body.errorMessage).toEqual('CPF is invalid')

      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should not find one client by cpf, client not found', async (done) => {
    try {
      const cpf = '10514756004'

      const result = await request(Server.app)
        .get(`/clients/cpf/${cpf}`)
        .expect(404)

      expect(result.body).not.toBe(null)
      const { body } = result

      expect(body.errorMessage).toEqual(`Client with CPF ${cpf} not found`)
      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should update client with success', async (done) => {
    try {
      const id = '1'

      await request(Server.app)
        .put(`/clients/${id}`)
        .send({
          name: 'other',
          birthDate: '1996-04-02T00:00:00-03:00'
        })
        .expect(204)

      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should not update client id not found', async (done) => {
    try {
      const id = '2'

      const result = await request(Server.app)
        .put(`/clients/${id}`)
        .send({
          name: 'other',
          birthDate: '1996-04-02T00:00:00-03:00'
        })
        .expect(404)

      expect(result.body).not.toBe(null)
      const { body } = result

      expect(body.errorMessage).toEqual(`Client with id ${id} not found`)
      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should not update client, body is invalid', async (done) => {
    try {
      const id = '1'

      const result = await request(Server.app)
        .put(`/clients/${id}`)
        .send({
          name: 'as',
          birthDate: '1996-04-02T00:00:00-03:00'
        })
        .expect(400)

      expect(result.body).not.toBe(null)
      const { body } = result
      console.log(body)

      expect(body.errorMessage).toEqual('The field name must be at least 3 chars')
      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should delete client with success', async (done) => {
    try {
      const id = '1'

      await request(Server.app)
        .delete(`/clients/${id}`)
        .expect(204)

      done()
    } catch (error) {
      done(error)
    }
  })

  it('Should not delete client, client not found', async (done) => {
    try {
      const id = '2'

      const result = await request(Server.app)
        .delete(`/clients/${id}`)
        .expect(404)

      expect(result.body).not.toBe(null)
      const { body } = result

      expect(body.errorMessage).toEqual(`Client with id ${id} not found`)
      done()
    } catch (error) {
      done(error)
    }
  })
})
