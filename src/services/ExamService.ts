import axios from "axios";

class ExamService {
  public static async index(): Promise<Array<Object>> {
    try {
      const { data } = await axios.get(
        "http://www.mocky.io/v2/5d681ede33000054e7e65c3f"
      );
      const examsAvailable = data.exams.map(exam => {
        return { id: exam.id, name: exam.name };
      });

      return examsAvailable;
    } catch (error) {
      throw new Error("Could not fetch exams");
    }
  }

  public static async show(id): Promise<Object> {
    try {
      const { data } = await axios.get(
        "http://www.mocky.io/v2/5d681ede33000054e7e65c3f"
      );
      return data.exams.find(exam => exam['id'] === id)

    } catch (error) {
      throw new Error("Could not fetch exams");
    }
  }
}

export default ExamService;
