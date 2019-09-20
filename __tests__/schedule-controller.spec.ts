import * as fs from 'fs'
import { createConnection, ConnectionOptions } from 'typeorm'
import * as request from 'supertest'

import Server from '../src/server'
import ExamIntegration from '../src/integrations/exam-integration'

import Client from '../src/entities/client'
import Exam from '../src/entities/exam'
import Schedule from '../src/entities/schedule'

describe('Make tests schedule controller', () => {
  let connection

  beforeAll(async (done) => {
    const options: ConnectionOptions = {
      type: 'sqlite',
      database: '__tests__/database-schedule-test.db',
      entities: [Exam, Schedule, Client],
      migrationsRun: true,
      migrations: ['migrations/*.ts']
    }

    connection = await createConnection(options)
    const clientRepository = connection.getRepository(Client)

    const client = new Client()
    client.cpf = '44846138836'
    client.name = 'Jeff'
    client.birthDate = new Date('1996-04-02T00:00:00-03:00')
    client.isActive = true

    await clientRepository.save(client)
    await ExamIntegration.importExams()
    Server.bootstrap()
    done()
  })

  afterAll((done) => {
    try {
      Server.close()
      connection.close()
      fs.unlinkSync('__tests__/database-schedule-test.db')
      done()
    } catch (error) {
      console.log('error', error)
      done(error)
    }
  })

  it('Should create schedule with success', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 2)
    finalDate.setHours(now.getHours() + 3)

    const bodyToCreate = {
      examId: 1,
      clientId: 1,
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    const result = await request(Server.app)
      .post('/schedules')
      .send(bodyToCreate)
      .expect(201)

    expect(result.body).not.toBe(null)

    const { body } = result
    expect(body.id).toEqual(1)
    done()
  })

  it('Should not create schedule, exam not found', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 2)
    finalDate.setHours(now.getHours() + 3)

    const bodyToCreate = {
      examId: 500,
      clientId: 1,
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    const result = await request(Server.app)
      .post('/schedules')
      .send(bodyToCreate)
      .expect(404)

    expect(result.body).not.toBe(null)

    const { body } = result
    expect(body.errorMessage).toEqual('Exam not found')
    done()
  })

  it('Should not create schedule, client not found', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 2)
    finalDate.setHours(now.getHours() + 3)

    const bodyToCreate = {
      examId: 1,
      clientId: 500,
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    const result = await request(Server.app)
      .post('/schedules')
      .send(bodyToCreate)
      .expect(404)

    expect(result.body).not.toBe(null)

    const { body } = result
    expect(body.errorMessage).toEqual('Client not found')
    done()
  })

  it('Should not create schedule, finalDate minor that initialDate ', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 3)
    finalDate.setHours(now.getHours() + 2)

    const bodyToCreate = {
      examId: 1,
      clientId: 1,
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    const result = await request(Server.app)
      .post('/schedules')
      .send(bodyToCreate)
      .expect(422)

    expect(result.body).not.toBe(null)

    const { body } = result
    expect(body.errorMessage).toEqual('The field finalDate is minor of initialDate')
    done()
  })

  it('Should not create schedule, initialDate minor that now ', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setDate(now.getDate() - 1)
    finalDate.setHours(now.getHours() + 2)

    const bodyToCreate = {
      examId: 1,
      clientId: 1,
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    const result = await request(Server.app)
      .post('/schedules')
      .send(bodyToCreate)
      .expect(422)

    expect(result.body).not.toBe(null)

    const { body } = result
    expect(body.errorMessage).toEqual('The field initialDate is minor of now')
    done()
  })

  it('Should not create schedule, schedule the same range ', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 2)
    finalDate.setHours(now.getHours() + 3)

    const bodyToCreate = {
      examId: 1,
      clientId: 1,
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    const result = await request(Server.app)
      .post('/schedules')
      .send(bodyToCreate)
      .expect(422)

    expect(result.body).not.toBe(null)

    const { body } = result
    expect(body.errorMessage).toEqual('Alredy exists schedule on date')
    done()
  })

  it('Should update schedule with success', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 4)
    finalDate.setHours(now.getHours() + 5)

    const id = 1
    const bodyToUpdate = {
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    await request(Server.app)
      .put(`/schedules/${id}`)
      .send(bodyToUpdate)
      .expect(204)
    done()
  })

  it('Should not update schedule, schedule not found', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 4)
    finalDate.setHours(now.getHours() + 5)

    const id = 2
    const bodyToUpdate = {
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    await request(Server.app)
      .put(`/schedules/${id}`)
      .send(bodyToUpdate)
      .expect(404)
    done()
  })

  it('Should not update schedule, schedule the same range', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 2)
    finalDate.setHours(now.getHours() + 3)

    const id = 2
    const bodyToUpdate = {
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    await request(Server.app)
      .put(`/schedules/${id}`)
      .send(bodyToUpdate)
      .expect(404)
    done()
  })

  it('Should not update schedule, sfinalDate minor that initialDate', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 2)
    finalDate.setHours(now.getHours() + 1)

    const id = 1
    const bodyToUpdate = {
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    await request(Server.app)
      .put(`/schedules/${id}`)
      .send(bodyToUpdate)
      .expect(422)
    done()
  })

  it('Should not update schedule, initialDate minor that now ', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setDate(now.getDate() - 1)
    finalDate.setHours(now.getHours() + 3)

    const id = 1
    const bodyToUpdate = {
      initialDate: initialDate.toISOString(),
      finalDate: finalDate.toISOString()
    }

    await request(Server.app)
      .put(`/schedules/${id}`)
      .send(bodyToUpdate)
      .expect(422)
    done()
  })

  it('Should delete schedule with success', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 4)
    finalDate.setHours(now.getHours() + 5)

    const id = 1

    await request(Server.app)
      .delete(`/schedules/${id}`)
      .expect(204)
    done()
  })

  it('Should find schedule by cpf of client with success', async (done) => {
    const now = new Date()
    const initialDate = new Date()
    const finalDate = new Date()

    initialDate.setHours(now.getHours() + 6)
    finalDate.setHours(now.getHours() + 7)

    const cpf = '44846138836'

    const clientRepository = connection.getRepository(Client)
    const scheduleRepository = connection.getRepository(Schedule)
    const examRepository = connection.getRepository(Exam)

    const client = await clientRepository.findOne(1)
    const exam = await examRepository.findOne(2)

    const schedule = new Schedule()

    schedule.exam = exam
    schedule.client = client
    schedule.initialDate = initialDate
    schedule.finalDate = finalDate
    schedule.isActive = true

    await scheduleRepository.save(schedule)

    const result = await request(Server.app)
      .get(`/clients/cpf/${cpf}/schedules`)
      .expect(200)
    console.log(result.body)
    done()
  })
})
