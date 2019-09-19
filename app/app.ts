import {Calls} from './utils/calls'
import { DatabaseOperations } from './utils/databaseOperations';
import { ExpressConf } from './conf/expressConf';

const app:any = new ExpressConf();
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
        agendamentos:[
            {
                "name":  "aa",
                "value": 35.6,
                "data":"yyyy-mm-dd"
                "horario":"hh:mm"
            }
        ]
    }
 */

 app.post('/new_client',async(req: any,resp: any) =>{
    let cliente = req.body;
    let ret:any = {}
    ret.AgendamentosRetorno = await await database.salvaAgendamento(cliente);
    ret.clienteCadastrado = await database.saveCliente(cliente);
    resp.json(await ret);
 })

 /** creation client example
 * {
        id:4,
        update:{
            nome:"aaa"
        }
    }
 */
 app.post('/update_client',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.updateCliente(cliente));
 })

 /** creation client example
 * {
        id:4
    }
 */
app.delete('/delete_client',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.deleteCliente(cliente));
 })

  /** creation client example
 * {
        "cpf":"00000000000"
    }
 */

app.post('/find_client',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.buscaCliente(cliente));
 })

 /** creation client example
 * {
        "count_last_record":0
    }
 */

app.post('/lista_client',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.listaClientes(cliente));
 })

/**
 * {
	"cpf":"00000001111"
    }
 */
 app.post('/find_agendamentos',async(req: any,resp: any) =>{
    let body = req.body
    resp.json(await database.buscaAgendamento(body));
 })

  /** creation client example
 * {
        id:4,
        update:{
            data:"aaa"
            horario:"aaa"
        }
    }
 */
 app.post('/update_agendamento',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.updateAgendamento(cliente));
 })


  /** creation client example
 * {
        id:4
    }
 */
app.delete('/delete_agendamento',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.deleteAgendamento(cliente));
 })

app.listen(9090,async ()=>{
    console.log('api is running');   
})