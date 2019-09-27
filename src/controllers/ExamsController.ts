import { Request, Response, Router } from "express";
import ExamService from "../services/ExamService";
class ExamController {
  private router = Router();
  constructor() {
    this.routes();
  }

  public routes() {
    this.router.get("/", this.index);
    return this.router;
  }

  private async index(req: Request, res: Response) {
    try {
      const exams = await ExamService.index();
      res.status(200).json({ exams });
    } catch (error) {
      res.json({ message: "Deu ruim" });
    }
  }
}

export default new ExamController();
