import CustomerService from '../../../src/services/customer'


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
        name: 'Luiz Filho',
        cpf: '464444466-44'
    }

    const service = new CustomerService()
    service.save(customer)

    const otherService = new CustomerService()
    const responseOtherService = otherService.save({ ...customer, name: 'João' })
    
    expect(responseOtherService.success).toEqual(false)
})

test('should update a customer to the database by service', () => {
    
    let customer = {
        name: 'Luiz Filho',
        cpf: '464444466-44'
    }

    const service = new CustomerService()
    service.save(customer)
    customer = { ...customer, name: 'João Pereira'}
    const customerUpdated = service.update(customer)

    expect(customerUpdated.data).toEqual(customer)
})

test('should not update a customer', () => {

    let customer = {
        name: 'Luiz Filho',
        cpf: '464444466-44'
    }

    const service = new CustomerService()
    service.save(customer)
    customer = { cpf: '464444466-46', name: 'João Pereira'}
    const customerUpdated = service.update(customer)

    expect(customerUpdated.success).toEqual(false)
})
