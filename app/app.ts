import express from 'express'
const app:any = express()

app.get('/',(req: any,resp: any)=>{
    resp.send('Hello World');
})

app.listen(9090,()=>{
    console.log('api is running');   
})