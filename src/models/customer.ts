import * as moment from "moment"

export interface Customer {
    name: String
    cpf: String
    dateOfBirth: moment.Moment
}