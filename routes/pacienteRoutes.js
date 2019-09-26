let pacienteClass = require("../classes/paciente")
var paciente = new pacienteClass();


/**
 * @swagger
 * /paciente/listar:
 *    get:
 *     tags:
 *       - paciente
 *     name: listar pacientes
 *     description: listar os pacientes
 *     summary: listar pacientes
 *     security:
 *       - bearerAuth: []
 *     consumes:
 *       - application/json
 *     produces:
 *       - application/json
 *     responses:
 *       200:
 *         description: Lista de Pacientes
 *       401:
 *         description: Erro
*/
app.get("/paciente/listar", (req, res) => {

    paciente.listar(req, res)
        .then(retorno => {
            res.status(200).json(retorno);
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});


/**
* @swagger
* /paciente/consultaPorCPF:
*   post:
*     tags:
*       - paciente
*     summary: consulta paciente por cpf
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
*         description: Sucesso
*        
*       401:
*         description: Erro
*/
app.post("/paciente/consultaPorCPF", (req, res) => {
    paciente.consultaPorCPF(req, res)
        .then(retorno => {
            res.status(200).json(retorno);
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});


/**
* @swagger
* /paciente/inserir:
*   post:
*     tags:
*       - paciente
*     summary: inserir pacientes
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
*             nome:
*               type: string
*             dataNascimento:
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
app.post("/paciente/inserir", (req, res) => {
    paciente.inserir(req, res)
        .then(retorno => {
            res.status(200).json("Paciente incluído com sucesso!");
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});

/**
* @swagger
* /paciente/alterar:
*   post:
*     tags:
*       - paciente
*     summary: alterar pacientes
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
*             nome:
*               type: string
*             dataNascimento:
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
app.post("/paciente/alterar", (req, res) => {
    paciente.alterar(req, res)
        .then(retorno => {
            res.status(200).json("Paciente alterado com sucesso!");
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});


/**
* @swagger
* /paciente/excluir:
*   post:
*     tags:
*       - paciente
*     summary: excluir pacientes
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
*         description: Sucesso
*        
*       401:
*         description: Erro
*/
app.post("/paciente/excluir", (req, res) => {
    paciente.excluir(req, res)
        .then(retorno => {
            res.status(200).json("Paciente excluído com sucesso!");
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});
