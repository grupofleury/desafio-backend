import CustomerService from '../../../src/services/customer'
import { cursorTo } from 'readline'


test('should save a customer to the database by service', () => {
    
    const customer = {
        name: 'Luiz Filho',
        cpf: '464444466-44'
    }

    const service = new CustomerService()
    const customerStored = service.save(customer)
    expect(customer).toEqual(customerStored.data)
})

test('should not save a customer', () => {

    const customer = {
        name: 'Luiz Carlos',
        cpf: '888555333-44'
    }

    const service = new CustomerService()
    service.save(customer)

    const otherService = new CustomerService()
    const responseOtherService = otherService.save({ ...customer, name: 'João Silva' })
    
    expect(responseOtherService.success).toEqual(false)
})

test('should update a customer to the database by service', () => {
    
    let customer = {
        name: 'Nathalia Souza',
        cpf: '961965000-44'
    }

    const service = new CustomerService()
    service.save(customer)
    customer = { ...customer, name: 'Geisiane Pereira'}
    const customerUpdated = service.update(customer)

    expect(customerUpdated.data).toEqual(customer)
})

test('should not update a customer', () => {

    let customer = {
        name: 'Hamilton Vicente',
        cpf: '464444466-44'
    }

    const service = new CustomerService()
    service.save(customer)
    customer = { cpf: '333161615-46', name: 'João Rodrigues'}
    const customerUpdated = service.update(customer)

    expect(customerUpdated.success).toEqual(false)
})

test('should remove a customer to the database by service', () => {
    
    let customer = {
        name: 'Fernanda Pereira',
        cpf: '464999966-44'
    }

    const service = new CustomerService()
    service.save(customer)
    const customerRemoved = service.remove(customer.cpf)

    expect(customerRemoved.success).toEqual(true)
    expect(customerRemoved.data).toEqual(customer)
})

test('should not delete a customer', () => {

    let customer = {
        name: 'Luiz Filho',
        cpf: '464444466-44'
    }

    const service = new CustomerService()
    service.save(customer)
    customer = { ...customer, cpf: '111222333-44' }
    const customerRemoved = service.remove(customer.cpf)

    expect(customerRemoved.success).toEqual(false)
})
