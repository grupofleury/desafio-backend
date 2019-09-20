import axios from 'axios'
import { getConnection } from 'typeorm'
import Exam from '../entities/exam'

class ExamIntegration {
  examAxios: any
  constructor () {
    this.examAxios = axios.create({
      baseURL: process.env.EXAM_URL || 'http://www.mocky.io/v2'
    })
  }

  async getExams () {
    const { data } = await this.examAxios.get('/5d681ede33000054e7e65c3f')
    return data.exams
  }

  async importExams () {
    const exams = await this.getExams()

    const lastSchedule = await getConnection()
      .getRepository(Exam)
      .createQueryBuilder('exam')
      .orderBy('externalId', 'DESC')
      .getOne()

    if (exams.length) {
      const examsToInsert = exams.filter(exam => {
        if (lastSchedule) {
          if (exam.id > parseInt(lastSchedule.externalId)) {
            exam.externalId = exam.id
            delete exam.id
            return exam
          }
        } else {
          exam.externalId = exam.id
          delete exam.id
          return exam
        }
      })

      await getConnection()
        .createQueryBuilder()
        .insert()
        .into(Exam)
        .values(examsToInsert)
        .execute()
    }
  }
}

export default new ExamIntegration()
