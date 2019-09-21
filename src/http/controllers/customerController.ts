import { Request, Response } from 'express'
import CustomerService from '../../services/customer'

class CustomerController {
    private service: CustomerService

    public constructor () {
        this.service = new CustomerService()
        this.save = this.save.bind(this)
    }

    public save(request: Request, response: Response) {
        const result =  this.service.save(request.body)
        response.send(result)
    }
}

export default CustomerController