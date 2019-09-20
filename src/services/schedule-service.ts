import { getRepository } from 'typeorm'

import Schedule from '../entities/schedule'
class ScheduleService {
  async dateIsValid (initialDate, finalDate) {
    const scheduleRepository = getRepository(Schedule)

    const now = new Date()
    initialDate = new Date(initialDate)
    finalDate = new Date(finalDate)

    if (finalDate < initialDate) {
      return {
        isValid: false,
        errorMessage: 'The field finalDate is minor of initialDate'
      }
    }

    if (initialDate < now) {
      return {
        isValid: false,
        errorMessage: 'The field initialDate is minor of now'
      }
    }

    const initialDateFormated = initialDate.toISOString().split('T').join(' ').replace('Z', '')
    const finalDateFormated = finalDate.toISOString().split('T').join(' ').replace('Z', '')

    const scheduleOnRange = await scheduleRepository
      .createQueryBuilder('schedule')
      .where('finalDate >= :initialDateFormated AND initialDate <= :finalDateFormated', {
        initialDateFormated,
        finalDateFormated
      })
      .getOne()

    if (scheduleOnRange) {
      return {
        isValid: false,
        errorMessage: 'Alredy exists schedule on date'
      }
    }

    return { isValid: true }
  }
}

export default new ScheduleService()
