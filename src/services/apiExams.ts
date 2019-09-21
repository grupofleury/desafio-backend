import request from 'request-promise-native'

class ApiExamsService {
    static async list(): Promise<Response> {
        return await request.get('http://www.mocky.io/v2/5d681ede33000054e7e65c3f')
    }
}

export default ApiExamsService