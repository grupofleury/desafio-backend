import { Sequelize } from "sequelize-typescript";
import { ApiError } from "../helpers/ApiError";
import Schedule from "../models/ScheduleModel";
import Customer from "../models/CustomerModel";
import ExamService from "./ExamService";
import UpdateScheduleSchema from "../validation/schedule/update.schema";
import CreateScheduleSchema from "../validation/schedule/create.schema";

class ScheduleService {
  public static async index() {
    const schedules = await Schedule.findAll({
      include: [{ model: Customer }]
    });
    return schedules;
  }

  public static async byDocument(cpf: string) {
    let totalPrice = 0;
    const schedule = await Schedule.findAll({
      include: [
        {
          model: Customer,
          where: {
            cpf
          }
        }
      ]
    });

    schedule.forEach(schd => {
      totalPrice += schd.price;
    });

    return Object.assign({
      totalPrice,
      schedule
    });
  }

  public static async store(body) {
    await CreateScheduleSchema.validate(body).catch(err => {
      throw new ApiError(err, 400);
    });

    const { customerId } = body;
    const maxClients = process.env.MAX_CLIENTS || 2;

    const customer = await Customer.findByPk(customerId);

    if (!customer) {
      throw new ApiError("Customer not found", 404);
    }

    const scheduleExists = await Schedule.findAll({
      where: {
        date: {
          [Sequelize.Op.eq]: body.date
        },
        hour: {
          [Sequelize.Op.eq]: body.hour
        },
        examId: {
          [Sequelize.Op.eq]: body.examId
        }
      }
    });

    if (scheduleExists.length >= maxClients) {
      throw new ApiError(
        "Date and hour is not available on Schedule, please choose another date or hour",
        409
      );
    }
    const exam = await ExamService.show(body.examId);
    
    if (!exam) {
      throw new ApiError("Exam not found", 404);
    }

    return await Schedule.create({
      ...body,
      price: exam["value"]
    });
  }

  public static async update(id: string, body) {
    await UpdateScheduleSchema.validate(body).catch(err => {
      throw new ApiError(err, 400);
    });
    const schedule = await Schedule.findByPk(id);

    if (!schedule) {
      throw new ApiError("Schedule not found", 404);
    }

    return await schedule
      .update({
        date: body.date,
        hour: body.hour
      })
      .catch(err => {
        throw new ApiError(err, 400);
      });
  }

  public static async delete(id) {
    const schedule = await Schedule.findByPk(id);

    if (!schedule) {
      throw new ApiError("Schedule not found", 404);
    }

    return await schedule.destroy({ force: true });
  }
}

export default ScheduleService;
