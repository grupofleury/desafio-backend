import { Request, Response } from 'express'
import CustomerService from '../../services/customer'

class CustomerController {
    private service: CustomerService

    public constructor () {
        this.service = new CustomerService()
        this.save = this.save.bind(this)
    }

    public save(request: Request, response: Response) {
        const customer =  this.service.save(request.body)

        if (!customer.success) {
            response.status(409)
        }
        response.send(customer)
    }
}

export default CustomerController