import * as Joi from 'joi';

export default class CreateScheduleSchema {
	private static schema = Joi.object().keys({
		customerId: Joi.number().required(),
		examId: Joi.number().required(),
    date: Joi.date().required(),
    hour: Joi.string().required(),
	});

	public static async validate(body): Promise<any> {
		return await Joi.validate(body, this.schema);
	}
}