import Server from "../../server";
import * as request from "supertest";

let app = null;

beforeAll(() => {
  const App = Server;
  App.listen(5000);
  app = App.app;
});

describe("Exams", () => {
  it("Shoud return all available exams", async () => {
    
    const exams = await request(app).get("/exams");
    expect(exams.body.exams).toBeInstanceOf(Array);
    expect(exams.body.exams[0]).toBeInstanceOf(Object);
    expect(exams.status).toBe(200);
  });
});
