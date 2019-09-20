import axios from 'axios'

class ApiExamsService {
    static async list(): Promise<Object[]> {
        let response = await axios.get('http://www.mocky.io/v2/5d681ede33000054e7e65c3f')        
        return  response.data.map( ({ id, name }: { id: Number, name: String}) => ({ id, name }))
    }
}

export default ApiExamsService