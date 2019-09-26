let exameClass = require("../classes/exame")
var exame = new exameClass();

/**
 * @swagger
 * /exame/listar:
 *    get:
 *     tags:
 *       - exame
 *     name: listar exames
 *     description: listar os exames
 *     summary: listar exames
 *     security:
 *       - bearerAuth: []
 *     consumes:
 *       - application/json
 *     produces:
 *       - application/json
 *     responses:
 *       200:
 *         description: Lista de Exames
 *       401:
 *         description: Erro
*/
app.get("/exame/listar", (req, res) => {

    exame.listar(req, res)
        .then(retorno => {
            res.status(200).json(retorno);
        })
        .catch(erro => {
            res.status(400).json(erro);
        })

});
