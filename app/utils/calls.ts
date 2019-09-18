import axios from 'axios'

export class Calls{
    
    async getListExams(){
        return await axios.get('http://www.mocky.io/v2/5d681ede33000054e7e65c3f');
    }


}