import database from '../../../data/db'

test('should save a client to the database', () => {
    const connection = database.connection()
    const customer = {
        name: 'Luiz Filho',
        cpf: '383383383-44'
    }

    const customerStored = connection.addCustomer(customer)
    const otherConnection = database.connection()
    const searched = otherConnection.findCustomer(customer.cpf)
    expect(customer).toEqual(searched.data)
    expect(customerStored.data).toEqual(searched.data)
})
