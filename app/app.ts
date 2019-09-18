import * as express from 'express'
import {Calls} from './utils/calls'
import { DatabaseOperations } from './utils/databaseOperations';
import * as bodyParser  from 'body-parser'

const app:any = express()
app.use( bodyParser.json() );       // to support JSON-encoded bodies
app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
  extended: true
}));
app.use(express.json());       // to support JSON-encoded bodies
app.use(express.urlencoded()); // to support URL-encoded bodies
const call:any = new Calls()
const database:any = new DatabaseOperations();

app.get('/list_exams',async(req: any,resp: any)=>{
    let response = await call.getListExams();
    resp.json(response.data);
})


/** creation client example
 * {
        nome:'jose',
        idade:18,
        email:'jase@joao.com',
        cpf:'00000000000',
        genero:0,
        endereco:'Rua xxxx Bairro XX numero 11',
        cidade:'sp'
    }
 */


 app.post('/new_client',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.saveCliente(cliente));
 })

app.listen(9090,async ()=>{
    console.log('api is running');   
})