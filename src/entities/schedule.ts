import { Entity, Column, PrimaryGeneratedColumn, ManyToOne } from 'typeorm'

import Client from './client'
import Exam from './exam'

@Entity()
export default class Schedule {
  @PrimaryGeneratedColumn()
  id: number

  @Column()
  initialDate: Date

  @Column()
  finalDate: Date

  @Column({
    default: true
  })
  isActive: boolean

  @Column({
    default: () => 'CURRENT_TIMESTAMP'
  })
  createdAt: Date

  @Column({
    default: () => 'CURRENT_TIMESTAMP'
  })
  updatedAt: Date

  @ManyToOne(type => Client, client => client.schedules)
  client: Client

  @ManyToOne(type => Exam, exam => exam.schedules)
  exam: Exam
}
