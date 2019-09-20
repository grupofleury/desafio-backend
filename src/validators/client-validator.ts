import { body } from 'express-validator'
import CpfValidator from '../validators/cpf-validator'

class UserValidator {
  createValidationBody () {
    return [
      body('cpf').custom(cpf => {
        return new Promise((resolve, reject) => {
          if (!CpfValidator.isValid(cpf)) {
            return reject(new Error('CPF is invalid'))
          }
          return resolve()
        })
      }),
      body('name', 'The field name must be at least 3 chars').isString().exists().isLength({ min: 3 }),
      body('birthDate', 'The field birthDate is invalid').isISO8601().exists()
    ]
  }

  updateValidationBody () {
    return [
      body('name', 'The field name must be at least 3 chars').isString().exists().isLength({ min: 3 }),
      body('birthDate', 'The field birthDate is invalid').isISO8601().exists()
    ]
  }
}

export default new UserValidator()
