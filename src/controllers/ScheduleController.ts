import { Request, Response, Router } from "express";
import ScheduleService from "../services/ScheduleService";

class ScheduleController {
  private router = Router();
  constructor() {
    this.routes();
  }

  public routes() {
    this.router.post("/", this.store);
    this.router.get("/", this.index);
    this.router.get("/document/:cpf", this.byDocument);
    this.router.put("/:id", this.update);
    this.router.delete("/:id", this.delete);
    return this.router;
  }

  private async index(req: Request, res: Response) {
    try {
      const schedules = await ScheduleService.index();
      res.status(200).json({ schedules });
    } catch (error) {
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }

  private async byDocument(req: Request, res: Response): Promise<Object> {
    try {
      const { cpf } = req.params;
      const schedulesByDocument = await ScheduleService.byDocument(cpf);

      return res.status(200).json(schedulesByDocument);
    } catch (error) {
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }

  private async store(req: Request, res: Response): Promise<any> {
    try {
      const schedule = await ScheduleService.store(req.body);

      res.status(201).json(schedule);
    } catch (error) {
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }

  private async update(req: Request, res: Response): Promise<any> {
    try {
      const { id } = req.params;
      await ScheduleService.update(id, req.body);
      res.status(204).json();
    } catch (error) {
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }

  private async delete(req: Request, res: Response): Promise<any> {
    try {
      const { id } = req.params;
      await ScheduleService.delete(id);

      res.status(204).json();
    } catch (error) {
      console.log(error);
      
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }
}

export default new ScheduleController();
