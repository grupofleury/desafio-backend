import DB from '../../../data/db'
import CustomerService from '../../../src/services/customer'
import Faker from 'faker'
import { copyFileSync } from 'fs'
import { getDefaultWatermarks } from 'istanbul-lib-report'

const getCpf = () => Faker.random.number({ 'min': 100000000,'max': 999999999 }) + '-' + Faker.random.number({ 'min': 10, 'max': 99 })

const fullName = () => Faker.name.firstName + ' ' + Faker.name.lastName

const getDate = () => Faker.date.between('1920-01-01', '2019-09-01')

let customer = {
    cpf: '',
    name: '',
    dateOfBirth: new Date()
}
let service = new CustomerService()

beforeEach(() => {
    DB.resetForUnitTest()
    customer = {
        name: fullName(),
        cpf: getCpf(),
        dateOfBirth: getDate()
    }
})

test('should save a customer to the database by service', () => {
    const customerStored = service.save(customer)
    expect(customer).toEqual(customerStored.data)
})

test('should not save a customer', () => {
    service.save(customer)
    const otherService = new CustomerService()
    const responseOtherService = otherService.save({ ...customer, name: fullName() })
    expect(responseOtherService.success).toEqual(false)
})

test('should update a customer to the database by service', () => {
    service.save(customer)
    customer = { ...customer, name: Faker.name.firstName + ' ' + fullName() }
    const customerUpdated = service.update(customer)
    expect(customerUpdated.data).toEqual(customer)
})

test('should not update a customer', () => {
    service.save(customer)
    customer = {
        cpf: getCpf(),
        name: fullName(),
        dateOfBirth: getDate()
    }
    const customerUpdated = service.update(customer)
    expect(customerUpdated.success).toEqual(false)
})

test('should remove a customer to the database by service', () => {
    service.save(customer)
    const customerRemoved = service.remove(customer.cpf)
    expect(customerRemoved.success).toEqual(true)
    expect(customerRemoved.data).toEqual(customer)
})

test('should not delete a customer', () => {
    service.save(customer)
    customer = {
        ...customer,
        cpf: getCpf()
     }
    const customerRemoved = service.remove(customer.cpf)

    expect(customerRemoved.success).toEqual(false)
})

test('should be returned a customer by cpf', () => {
    service.save(customer)
    const customerSeached = service.find(customer.cpf)
    expect(customerSeached.success).toEqual(true)
    expect(customerSeached.data).toEqual(customer)
})

test('should not return a customer by cpf', () => {
    service.save(customer)
    customer.cpf = getCpf()
    const customerSeached = service.find(customer.cpf)

    expect(customerSeached.success).toEqual(false)
    expect(customerSeached.data).toEqual(null)
})

test('should returned all customers', () => {
    const service = new CustomerService()
    for(let index = 0; index < 10; index++) {
        let customer = {
            cpf: getCpf(),
            name: fullName()
        }

        service.save(customer)
    }

    let customers = service.list()
    expect(customers.data.length).toEqual(10)

})
