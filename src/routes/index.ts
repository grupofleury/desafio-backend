import { Router, Request, Response } from "express";
import CustomerController from "../controllers/CustomerController";
import ScheduleController from "../controllers/ScheduleController";
import ExamController from "../controllers/ExamsController";

const router = Router();

router.use("/info", (req: Request, res: Response) => {
  res.json({
    message: "API VERSION 1.0"
  });
});
router.use("/customers", CustomerController.routes());
router.use("/schedule", ScheduleController.routes());
router.use("/exams", ExamController.routes());

export default router;
