import { Router } from 'express'
import   { CustomerController }   from '../http/controllers/index'

const CustomerRoutes = Router()
const controller = new CustomerController()
CustomerRoutes.post('/customers', controller.save)
CustomerRoutes.put('/customers/:cpf', controller.update)
CustomerRoutes.delete('/customers/:cpf', controller.remove)
CustomerRoutes.get('/customers/:cpf', controller.find)

export  { CustomerRoutes }