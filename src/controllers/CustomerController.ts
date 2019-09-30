import { Request, Response, Router } from "express";
import CustomerService from "../services/CustomerService";

class CustomerController {
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
      const customers = await CustomerService.index();
      res.status(200).json({ customers });
    } catch (error) {
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }

  private async byDocument(req: Request, res: Response): Promise<any> {
    try {
      const { cpf } = req.params;
      const customer = await CustomerService.show(cpf);

      res.status(200).json(customer);
      return;
    } catch (error) {
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }

  private async store(req: Request, res: Response): Promise<any> {
    try {
      const customer = await CustomerService.store(req.body);

      res.status(201).json(customer);
      return customer;
    } catch (error) {
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }

  private async update(req: Request, res: Response): Promise<any> {
    try {
      const { id } = req.params;

      await CustomerService.update(id, req.body);

      res.status(204).json();
    } catch (error) {
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }

  private async delete(req: Request, res: Response): Promise<any> {
    try {
      const { id } = req.params;

      await CustomerService.delete(id);

      res.status(204).json();
    } catch (error) {
      const { name: message, statusCode } = error;
      res.status(statusCode || 500).json({ message });
    }
  }
}

export default new CustomerController();
