const express = require("express");
var bodyParser = require('body-parser');
app = express();
app.use(bodyParser.json());
request = require('request');
router = express.Router();

numAgendamentosRepetidos = 1;

require("./bd");
require("./routes/swaggerRoutes");
require("./routes/pacienteRoutes");
require("./routes/exameRoutes");
require("./routes/agendamentoRoutes");

var PORT = 8085;
app.listen(PORT, () => {
    console.log(`Serviço FleuryAPI iniciado na porta:${PORT}...`);
    console.log(`Para ver o swagger: http://localhost:${PORT}/swagger`);    
});
