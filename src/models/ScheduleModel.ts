import { Model, PrimaryKey, Column, AutoIncrement, Table, DataType, ForeignKey, BelongsTo } from "sequelize-typescript";
import Customer from "./CustomerModel";


@Table({
  tableName: "schedule",
  underscoredAll: true,
  underscored: true,
  timestamps: true,
  paranoid: true,
})

export default class Schedule extends Model<Schedule> {
  @PrimaryKey
  @AutoIncrement
  @Column
  public id?: number;

  @Column
  public examId: number;

  @Column
  public price: number;

  @Column({ type: DataType.DATEONLY, allowNull: false})
  public date: string;

  @Column({ type: DataType.TIME, allowNull: false})
  public hour: string;

  @ForeignKey(() => Customer)
  @Column({ field: "customer_id" })
  public customerId: number;

  @BelongsTo(() => Customer)
  public customer: Customer;

}
