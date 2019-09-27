import { Model, PrimaryKey, Column, AutoIncrement, Table, Unique } from "sequelize-typescript";


@Table({
  tableName: "customers",
  underscoredAll: true,
  underscored: true,
  timestamps: true,
  paranoid: true,
})

export default class Customer extends Model<Customer> {
  @PrimaryKey
  @AutoIncrement
  @Column
  public id?: number;

  @Column
  public name: string;

  @Unique
  @Column
  public cpf: string;

  @Column
  public birthDate: Date;
}
