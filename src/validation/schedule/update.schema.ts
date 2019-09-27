import * as Joi from 'joi';

export default class UpdateScheduleSchema {
	private static schema = Joi.object().keys({
    date: Joi.date().optional(),
    hour: Joi.string().optional()
	});

	public static async validate(body): Promise<any> {
		return await Joi.validate(body, this.schema);
	}
}