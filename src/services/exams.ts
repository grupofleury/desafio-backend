import axios from 'axios'

class ExamsService {
    static list() {
        return axios.get('http://www.mocky.io/v2/5d681ede33000054e7e65c3f').then( (response: any) => {
            return response.data.map( ({ id, name }: { id: Number, name: String}) => ({ id, name }))
        })
    }
}

export default ExamsService