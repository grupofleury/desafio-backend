import { Entity, Column, PrimaryGeneratedColumn, OneToMany } from 'typeorm'

import Schedule from './schedule'

@Entity()
export default class Exam {
  @PrimaryGeneratedColumn()
  id: number;

  @Column()
  name: string;

  @Column()
  value: number;

  @Column()
  externalId: string;

  @OneToMany(type => Schedule, schedule => schedule.exam)
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
