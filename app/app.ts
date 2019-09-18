import express from 'express'
import {Calls} from './utils/calls'

const app:any = express()
const call:any = new Calls();

app.get('/list_exams',async(req: any,resp: any)=>{
    let response = await call.getListExams();
    resp.json(response.data);
})

app.listen(9090,()=>{
    console.log('api is running');   
})