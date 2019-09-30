import Server from "../../server";
import * as faker from "faker";
import * as fakerCPF from "cpf_cnpj";
import * as request from "supertest";

const payload = {
  name: `${faker.name.firstName()} ${faker.name.lastName()}`,
  cpf: fakerCPF.CPF.generate(),
  birthDate: faker.date.past()
};
let app = null;

beforeAll(() => {
  const App = Server;
  App.listen(5000);
  app = App.app;
});

describe("Customers", () => {
  // @ts-ignore
  let customerId = null;
  it("Shoud create an Customer", async () => {
    const customer = await request(app)
      .post("/customers")
      .send({ ...payload });

    customerId = customer.body.id;
    expect(customer.status).toBe(201);
  });

  it("Shoud update an Customer", async () => {
    const customer = await request(app)
      .put(`/customers/${customerId}`)
      .send({ name: faker.name.firstName() });
    expect(customer.status).toBe(204);
  });

  it("Shoud return all Customers", async () => {
    const customer = await request(app).get("/customers");
    expect(customer.body.customers).toBeInstanceOf(Array);
    expect(customer.status).toBe(200);
  });

  it("Shoud return an customers by document", async () => {
    const customer = await request(app).get(
      `/customers/document/${payload.cpf}`
    );
    expect(customer.body).toBeInstanceOf(Object);
    expect(customer.status).toBe(200);
  });

  it("Shoud delete an customers by id", async () => {
    const customer = await request(app).delete(`/customers/${customerId}`);
    expect(customer.body).toBeInstanceOf(Object);
    expect(customer.status).toBe(204);
  });
});
