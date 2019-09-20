import { Entity, Column, PrimaryGeneratedColumn, OneToMany } from 'typeorm'

import Schedule from './schedule'

@Entity()
export default class Client {
  @PrimaryGeneratedColumn()
  id: number

  @Column({
    unique: true
  })
  cpf: string

  @Column()
  birthDate: Date

  @Column()
  name: string

  @Column({
    default: true
  })
  isActive: boolean

  @OneToMany(type => Schedule, schedule => schedule.client)
  schedules: Schedule[];

  @Column({
    default: () => 'CURRENT_TIMESTAMP'
  })
  createdAt: Date;

  @Column({
    default: () => 'CURRENT_TIMESTAMP'
  })
  updatedAt: Date;
}
