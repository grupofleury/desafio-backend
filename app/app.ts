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

 app.post('/new_client',async(req: any,resp: any) =>{
    let cliente = req.body;
    let ret:any = {}
    let existClient = await database.existCliente(cliente);
    if(existClient){
        resp.send('cpf ja registrado');
    }else{
    ret.clienteCadastrado = await database.saveCliente(cliente);
    resp.json(await ret);
    }
 })

 app.post('/update_client',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.updateCliente(cliente));
 })

app.delete('/delete_client',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.deleteCliente(cliente));
 })

app.post('/find_client',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.buscaCliente(cliente));
 })

app.post('/lista_cliente',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.listaClientes(cliente));
 })

 app.post('/find_agendamentos',async(req: any,resp: any) =>{
    let body = req.body
    resp.json(await database.buscaAgendamento(body));
 })

 app.post('/update_agendamento',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.updateAgendamento(cliente));
 })

app.delete('/delete_agendamento',async(req: any,resp: any) =>{
    let cliente = req.body
    resp.json(await database.deleteAgendamento(cliente));
 })

 app.post('/agendamento_cliente',async(req: any,resp: any) =>{
    let cliente = req.body
    let existClient = await database.existCliente(cliente);
    let existAgendamento = await database.existAgendamento(cliente);
    console.log(existAgendamento)
    switch(existAgendamento) { 
        case false : { 
            if(existClient){
                resp.json(await database.salvaAgendamento(cliente));
            }else{
                resp.json('cliente não encontrado, favor realizar cadastro');
            }
           break; 
        } 
        case true : { 
            resp.send('Já existem dois pacientes p/ essa data e horário');
           break; 
        }
    } 
        
 })


app.listen(9090,async ()=>{
    console.log('api is running');   
})