let agendamentoClass = require("../classes/agendamento")
var agendamento = new agendamentoClass();


/**
* @swagger
* /agendamento/consultaPorCPF:
*   post:
*     tags:
*       - agendamento
*     summary: consulta agendamento por cpf
*     security:
*       - bearerAuth: []
*     consumes:
*       - application/json
*     produces:
*       - application/json
*     parameters:
*       - name: body
*         in: body
*         type: string
*         required: true
*         schema:
*           type: object
*           properties:
*             cpf:
*               type: string
*     responses:
*       200:
*         description: Agendamento
*        
*       401:
*         description: Erro
*/
app.post("/agendamento/consultaPorCPF", (req, res) => {

    agendamento.consultaPorCPF(req, res)
        .then(retorno => {
            res.status(200).json(retorno);
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});

/**
* @swagger
* /agendamento/inserir:
*   post:
*     tags:
*       - agendamento
*     summary: inserir agendamento
*     security:
*       - bearerAuth: []
*     consumes:
*       - application/json
*     produces:
*       - application/json
*     parameters:
*       - name: body
*         in: body
*         type: string
*         required: true
*         schema:
*           type: object
*           properties:
*             idExame:
*               type: integer
*             dataHora:
*               type: string
*             cpf:
*               type: string
*     responses:
*       200:
*         description: Sucesso
*        
*       401:
*         description: Erro
*/
app.post("/agendamento/inserir", (req, res) => {
    agendamento.inserir(req, res)
        .then(retorno => {
            res.status(200).json("Agendamento incluído com sucesso!");
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});


/**
* @swagger
* /agendamento/alterar:
*   post:
*     tags:
*       - agendamento
*     summary: alterar agendamento
*     security:
*       - bearerAuth: []
*     consumes:
*       - application/json
*     produces:
*       - application/json
*     parameters:
*       - name: body
*         in: body
*         type: string
*         required: true
*         schema:
*           type: object
*           properties:
*             dataHora:
*               type: string
*             cpf:
*               type: string
*     responses:
*       200:
*         description: Sucesso
*        
*       401:
*         description: Erro
*/
app.post("/agendamento/alterar", (req, res) => {
    agendamento.alterar(req, res)
        .then(retorno => {
            res.status(200).json("Agendamento alterado com sucesso!");
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});

/**
* @swagger
* /agendamento/excluir:
*   post:
*     tags:
*       - agendamento
*     summary: excluir agendamento
*     security:
*       - bearerAuth: []
*     consumes:
*       - application/json
*     produces:
*       - application/json
*     parameters:
*       - name: body
*         in: body
*         type: string
*         required: true
*         schema:
*           type: object
*           properties:
*             dataHora:
*               type: string
*             cpf:
*               type: string
*     responses:
*       200:
*         description: Sucesso
*        
*       401:
*         description: Erro
*/
app.post("/agendamento/excluir", (req, res) => {
    agendamento.excluir(req, res)
        .then(retorno => {
            res.status(200).json("Agendamento excluído com sucesso!");
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});