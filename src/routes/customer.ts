import { Router } from 'express'
import  CustomerController  from '../http/controllers/customerController'

const CustomerRoutes = Router()
const controller = new CustomerController()
CustomerRoutes.post('/customers', controller.save)
CustomerRoutes.put('/customers', controller.update)
CustomerRoutes.delete('/customers/:cpf', controller.remove)

export  { CustomerRoutes }