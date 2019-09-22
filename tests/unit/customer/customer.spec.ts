import DB from '../../../data/db'
import { CustomerService } from '../../../src/services/index'
import { getCpf, fullName, getDate } from '../utils/dataGenerate'

let customer = {
    cpf: '',
    name: '',
    dateOfBirth: ''
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

describe('Customer save', () => {
    it('should save a customer to the database by service', () => {
        const customerStored = service.save(customer)
        expect(customer).toEqual(customerStored.data)
    })
    
    it('should not save a customer', () => {
        service.save(customer)
        const otherService = new CustomerService()
        const responseOtherService = otherService.save({ ...customer, name: fullName() })
        expect(responseOtherService.success).toEqual(false)
    })
})

describe('Customer update', () => {
    it('should update a customer to the database by service', () => {
        service.save(customer)
        customer = { ...customer, name: fullName() }
        const customerUpdated = service.update(customer.cpf, customer)
        expect(customerUpdated.data).toEqual(customer)
    })
    
    it('should not update a customer', () => {
        service.save(customer)
        customer = {
            cpf: getCpf(),
            name: fullName(),
            dateOfBirth: getDate()
        }
        const customerUpdated = service.update(customer.cpf, customer)
        expect(customerUpdated.success).toEqual(false)
    })
})

describe('Customer remove', () => {
    it('should remove a customer to the database by service', () => {
        service.save(customer)
        const customerRemoved = service.remove(customer.cpf)
        expect(customerRemoved.success).toEqual(true)
        expect(customerRemoved.data).toEqual(customer)
    })
    
    it('should not delete a customer', () => {
        service.save(customer)
        customer = {
            ...customer,
            cpf: getCpf()
         }
        const customerRemoved = service.remove(customer.cpf)
    
        expect(customerRemoved.success).toEqual(false)
    })
})

describe('Customer read', () => {
    it('should be returned a customer by cpf', () => {
        service.save(customer)
        const customerSeached = service.find(customer.cpf)
        expect(customerSeached.success).toEqual(true)
        expect(customerSeached.data).toEqual(customer)
    })
    
    it('should not return a customer by cpf', () => {
        service.save(customer)
        customer.cpf = getCpf()
        const customerSeached = service.find(customer.cpf)
    
        expect(customerSeached.success).toEqual(false)
        expect(customerSeached.data).toEqual(null)
    })
    
    it('should returned all customers', () => {
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
})

