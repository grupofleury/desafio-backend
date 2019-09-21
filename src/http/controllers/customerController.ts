import { Request, Response } from 'express'
import CustomerService from '../../services/customer'

class CustomerController {
    private service: CustomerService

    public constructor () {
        this.service = new CustomerService()
        this.save = this.save.bind(this)
        this.update = this.update.bind(this)
        this.remove = this.remove.bind(this)
        this.find = this.find.bind(this)
    }

    public save(request: Request, response: Response) {
        const customer =  this.service.save(request.body)

        if (!customer.success) {
            response.status(409)
        }
        response.send(customer)
    }

    public update(request: Request, response: Response) {
        const customer =  this.service.update(request.body)

        if (!customer.success) {
            response.status(404)
        }
        response.send(customer)
    }

    public remove(request: Request, response: Response) {
        const customer =  this.service.remove(request.params.cpf)

        if (!customer.success) {
            response.status(409)
        }
        response.send(customer)
    }

    public find(request: Request, response: Response) {
        const customer =  this.service.find(request.params.cpf)

        if (!customer.success) {
            response.status(404)
        }
        response.send(customer)
    }
}

export default CustomerController