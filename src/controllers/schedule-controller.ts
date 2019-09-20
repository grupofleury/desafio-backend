import { getRepository } from 'typeorm'

import ScheduleService from '../services/schedule-service'

import Exam from '../entities/exam'
import Client from '../entities/client'
import Schedule from '../entities/schedule'

class ScheduleController {
  async create (req, res) {
    const { body } = req

    try {
      const { examId, clientId, initialDate, finalDate } = body

      const examRepository = getRepository(Exam)
      const clientRepository = getRepository(Client)
      const scheduleRepository = getRepository(Schedule)

      const exam = await examRepository.findOne(examId)

      if (!exam) {
        return res.status(404).send({ errorMessage: 'Exam not found' })
      }

      const client = await clientRepository.findOne(clientId)

      if (!client) {
        return res.status(404).send({ errorMessage: 'Client not found' })
      }

      if (!client.isActive) {
        return res.status(422).send({ errorMessage: 'Client not active' })
      }

      const dataIsValidValidation = await ScheduleService.dateIsValid(initialDate, finalDate)

      if (!dataIsValidValidation.isValid) {
        return res.status('422').send({ errorMessage: dataIsValidValidation.errorMessage })
      }

      const schedule = new Schedule()

      schedule.initialDate = new Date(initialDate)
      schedule.finalDate = new Date(finalDate)
      schedule.exam = exam
      schedule.client = client
      schedule.isActive = true

      const { id } = await scheduleRepository.save(schedule)
      return res.status(201).send({ id })
    } catch (error) {
      return res.status(500).send({ errorMessage: error.message })
    }
  }

  async update (req, res) {
    const { body } = req
    const { id } = req.params

    try {
      const { initialDate, finalDate } = body
      const scheduleRepository = getRepository(Schedule)
      const schedule = await scheduleRepository.findOne(id)

      if (!schedule) {
        return res.status(404).send({ errorMessage: 'Schedule not found' })
      }

      const dataIsValidValidation = await ScheduleService.dateIsValid(initialDate, finalDate)

      if (!dataIsValidValidation.isValid) {
        return res.status('422').send({ errorMessage: dataIsValidValidation.errorMessage })
      }

      schedule.initialDate = initialDate
      schedule.finalDate = finalDate

      await scheduleRepository.save(schedule)
      return res.status(204).send()
    } catch (error) {
      return res.status(500).send({ errorMessage: error.message })
    }
  }

  async delete (req, res) {
    const { id } = req.params

    try {
      const scheduleRepository = getRepository(Schedule)
      const schedule = await scheduleRepository.findOne(id)

      if (!schedule) {
        return res.status(404).send({ errorMessage: `User with id ${id} not found` })
      }

      schedule.isActive = false
      await scheduleRepository.save(schedule)
      return res.status(204).send()
    } catch (error) {
      return res.status(500).send({ errorMessage: error.message })
    }
  }

  async findByCPF (req, res) {
    const { cpf } = req.params

    try {
      const scheduleRepository = getRepository(Schedule)
      const schedules = await scheduleRepository
        .createQueryBuilder('schedule')
        .select('schedule.initialDate, schedule.finalDate, schedule.id, schedule.isActive, exam.name, exam.value, client.name, client.cpf')
        .innerJoin('schedule.client', 'client')
        .innerJoin('schedule.exam', 'exam')
        .where('client.cpf = :cpf', { cpf })
        .andWhere('schedule.isActive = :isActive', { isActive: true })
        .execute()

      let totalValue = 0

      for (const schedule of schedules) {
        totalValue += schedule.value
      }

      return res.json({ schedules, totalValue })
    } catch (error) {
      return res.status(500).send({ errorMessage: error.message })
    }
  }
}

export default new ScheduleController()
