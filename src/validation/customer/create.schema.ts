import * as Joi from 'joi';

export default class CreateCustomerSchema {
	private static schema = Joi.object().keys({
		name: Joi.string().required(),
		cpf: Joi.string().length(11).required(),
    birthDate: Joi.date().required(),
	});

	public static async validate(body): Promise<any> {
		return await Joi.validate(body, this.schema);
	}
}