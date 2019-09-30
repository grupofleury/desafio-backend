import * as Joi from 'joi';

export default class UpdateCustomerSchema {
	private static schema = Joi.object().keys({
		name: Joi.string().optional(),
		cpf: Joi.string().length(11).optional(),
    birthDate: Joi.date().optional(),
	});

	public static async validate(body): Promise<any> {
		return await Joi.validate(body, this.schema);
	}
}