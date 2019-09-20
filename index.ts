import { createConnection } from 'typeorm'
import Server from './src/server'
import ExamIntegration from './src/integrations/exam-integration'

(async () => {
  await createConnection()
  await ExamIntegration.importExams()
  await Server.bootstrap()
})()
