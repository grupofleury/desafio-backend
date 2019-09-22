import { Exam } from "../models/exam";

class Formatter {
    public static extractFields(data: Exam[]): Exam[] {
        return data.map(({ id, name }: Exam) => ({ id, name }))
    }
}

export default Formatter