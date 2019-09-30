import Customer from '../models/CustomerModel';
import { ApiError } from '../helpers/ApiError';
import CreateCustomerSchema from '../validation/customer/create.schema';
import UpdateCustomerSchema from '../validation/customer/update.schema';

export default class CustomerService {
  public static async index() {
    return await Customer.findAll();
  }

  public static async show(cpf: string) {
    return await Customer.findOne({
      where: {
        cpf
      }
    }).catch(err => {
      throw new ApiError(err, 500);
    });
  }

  public static async store(body: Object) {
    await CreateCustomerSchema.validate(body).catch(err => {
      throw new ApiError(err, 400);
    });

    return await Customer.create({
      ...body
    }).catch(err => {
      throw new ApiError(err, 400);
    });
  }

  public static async update(id: string, body: Object) {
    await UpdateCustomerSchema.validate(body).catch(err => {
      throw new ApiError(err, 400);
    });

    const customer = await Customer.findByPk(id);
    if (!customer) {
      throw new ApiError('Customer not found', 404);
    }

    return await customer
      .update({
        ...body
      })
      .catch(err => {
        throw new ApiError(err, 500);
      });
  }

  public static async delete(id) {
    const customer = await Customer.findByPk(id);

    if (!customer) {
      throw new ApiError('Customer not found', 404);
    }

    await customer.destroy({ force: true });
  }
}
