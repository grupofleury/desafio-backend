import CustomerService from '../../../src/services/customer'


test('should save a client to the database by service', () => {
    
    const customer = {
        name: 'Luiz Filho',
        cpf: '464444466-44'
    }

    const service = new CustomerService()
    const customerStored = service.save(customer)
    expect(customer).toEqual(customerStored)
})

test('should return null and not save a client', () => {

    const customer = {
        name: 'Luiz Filho',
        cpf: '464444466-44'
    }

    const service = new CustomerService()
    service.save(customer)

    const otherService = new CustomerService()
    const responseOtherService = otherService.save({ ...customer, name: 'João' })
    
    expect(responseOtherService).toEqual(null)
})
