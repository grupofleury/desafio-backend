import Server from "../../server";
import * as faker from "faker";
import * as fakerCPF from 'cpf_cnpj';
import * as request from "supertest";

const customerPayload = {
  name: `${faker.name.firstName()} ${faker.name.lastName()}`,
  cpf: fakerCPF.CPF.generate(),
  birthDate: faker.date.past()
};
const payload = {
  customerId: null,
  examId: faker.random.number({ min: 5, max: 10 }).toString(),
  date: faker.date.future(),
  hour: `${new Date().getHours()}:${new Date().getMinutes()}`
};
let app = null;

beforeAll(() => {
  const App = Server;
  App.listen(5000);
  app = App.app;
});

describe("Schedules", () => {
  // @ts-ignore
  let scheduleId = null;
  const maxClients = parseInt(process.env.MAX_CLIENTS);

  it("Should create an Customer", async () => {
    const customer = await request(app)
      .post("/customers")
      .send({ ...customerPayload });

    payload.customerId = customer.body.id;
    expect(customer.status).toBe(201);
  });

  it("Should create an Schedule", async () => {
    const schedule = await request(app)
      .post("/schedule")
      .send({ ...payload });

    scheduleId = schedule.body.id;

    expect(schedule.status).toBe(201);
  });

  it("Should return all Schedules", async () => {
    const schedule = await request(app).get("/schedule");
    expect(schedule.body.schedules).toBeInstanceOf(Array);
    expect(schedule.status).toBe(200);
  });

  it("Should update an schedule", async () => {
    const schedule = await request(app)
      .put(`/schedule/${scheduleId}`)
      .send({
        hour: `${new Date().getHours()}:${new Date().getMinutes()}`,
        date: new Date()
      });
    expect(schedule.status).toBe(204);
  });

  it(`Should not allow to create more than ${maxClients} schedules per exam, date and hour`, async () => {
    let schedule = null;

    await new Promise(async (resolve, reject) => {
      for (let i = 0; i <= maxClients; i++) {
        schedule = await request(app)
          .post("/schedule")
          .send({ ...payload });
      }
      resolve()
    });
    
    expect(schedule.error.status).toBe(409);
  });

  it("Should return all schedules by customer document", async () => {
    const schedule = await request(app).get(
      `/schedule/document/${customerPayload.cpf}`
    );

    expect(schedule.body).toBeInstanceOf(Object);
    expect(schedule.status).toBe(200);
  });

  it("Should delete an schedule by id", async () => {
    const schedule = await request(app).delete(`/schedule/${scheduleId}`);
    expect(schedule.body).toBeInstanceOf(Object);
    expect(schedule.status).toBe(204);
  });
});
