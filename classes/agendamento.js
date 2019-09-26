let exameClass = require("../classes/exame")
var exame = new exameClass();

var utilClass = require("../classes/utils")
var utils = new utilClass();

class agendamento {

    consultaPorCPF(req) {
        return new Promise((resolve, reject) => {

            if (req.body.cpf) {
                let cpf = req.body.cpf;

                bd.collection("agendamentos").where("cpf", "==", cpf).get()
                    .then(snapshot => {
                        let dados = snapshot.docs.map(doc => doc.data());

                        if (dados.length > 0) {
                            resolve(dados[0]);
                        }
                        else {
                            reject("Não foi encontrado agendamento com o cpf " + cpf);

                        }

                    })
                    .catch(erro => {
                        reject('Erro ao retornar agendamento.' + erro)
                    });
            }
        });
    }

    inserir(req, res) {
        return new Promise((resolve, reject) => {
            let campos = {};
            let erros = [];

            if (req.body.idExame) {
                campos.idExame = req.body.idExame;

                exame.listar()
                    .then((retorno) => {
                        var existe = retorno.Exames.filter(x => x.id == campos.idExame);

                        if (existe.length == 0)
                            throw Error("Erro ao inserir agendamento. Id do Exame inválido.");

                        if (req.body.dataHora) {
                            campos.dataHora = req.body.dataHora;
                        }
                        else
                            erros.push("dataHora");

                        if (req.body.cpf) {
                            campos.cpf = req.body.cpf;

                            if (!utils.ValidaCPF(campos.cpf)) {
                                throw Error("Erro ao inserir agendamento. CPF inválido.");
                            }
                        }
                        else
                            erros.push("cpf");

                        campos.ativo = "true";
                        campos.dataCriacao = firebase.firestore.FieldValue.serverTimestamp();
                        campos.dataAlteracao = firebase.firestore.FieldValue.serverTimestamp();

                        if (erros.length > 0) {
                            throw Error("Erro ao inserir agendamento. Campo " + erros[0] + " é obrigatório.");
                        }

                        this.verificaSeExistePaciente(req, res)
                            .then(existe => {

                                bd.collection("agendamentos").add(campos)
                                    .then(snapshot => {
                                        resolve();
                                    })
                                    .catch(function (error) {
                                        reject('Erro ao inserir agendamento. ' + error);
                                    });

                            })
                            .catch(function (error) {
                                reject('Erro ao inserir agendamento. ' + error);
                            });

                    })
                    .catch((erro) => {
                        reject(erro.message);
                    })

            }
            else
                erros.push("idExame");
        })

    }

    alterar(req, res) {
        return new Promise((resolve, reject) => {
            let campos = {};
            let erros = [];

            if (req.body.cpf) {

                if (!utils.ValidaCPF(req.body.cpf)) {
                    reject("Erro ao alterar agendamento. CPF inválido.");
                    return;
                }
            }
            else
                erros.push("cpf");

            if (req.body.dataHora) {
                campos.dataHora = req.body.dataHora;
            }
            else
                erros.push("dataHora");

            campos.dataAlteracao = firebase.firestore.FieldValue.serverTimestamp();

            if (erros.length > 0) {
                reject("Erro ao alterar agendamento. Campo " + erros[0] + " é obrigatório.");
                return;
            }

            if (req.body.cpf && req.body.cpf != "" && req.body.cpf != 0) {
                bd.collection("agendamentos").where("cpf", "==", req.body.cpf).where("dataHora", "==", campos.dataHora).get()
                    .then(snapshot => {

                        let dados = snapshot.docs.map(doc => doc.data());
                        if (dados.length > 0) {

                            let id = snapshot.docs[0].id;
                            bd.collection("agendamentos").doc(id).update(campos)
                                .then(snapshot => {
                                    resolve()
                                })
                                .catch(function (error) {
                                    reject("Erro ao alterar agendamento. " + error);
                                    return;
                                });

                        }
                        else {
                            reject("Não foi encontrado agendamento com o cpf " + req.body.cpf + " e com datahora " + campos.dataHora);
                            return;
                        }

                    })
                    .catch(function (error) {
                        reject("Erro ao alterar paciente." + error);
                        return;
                    });
            }

        });

    }

    excluir(req, res) {
        return new Promise((resolve, reject) => {
            let campos = {};
            let erros = [];

            if (req.body.cpf) {
                campos.cpf = req.body.cpf;

                if (!utils.ValidaCPF(campos.cpf)) {
                    reject("Erro ao excluir agendamento. CPF inválido.");
                    return;
                }
            }
            else
                erros.push("cpf");


            if (req.body.dataHora) {
                campos.dataHora = req.body.dataHora;
            }
            else
                erros.push("dataHora");


            if (erros.length > 0) {
                reject("Erro ao excluir agendamento. Campo " + erros[0] + " é obrigatório.");
                return;
            }

            if (campos.cpf && campos.cpf != "" && campos.cpf != 0) {
                bd.collection("agendamentos").where("cpf", "==", campos.cpf).where("dataHora", "==", campos.dataHora).get()
                    .then(snapshot => {

                        let dados = snapshot.docs.map(doc => doc.data());
                        if (dados.length > 0) {

                            let id = snapshot.docs[0].id;
                            bd.collection("agendamentos").doc(id).delete();
                            resolve();
                        }
                        else {
                            reject("Não foi encontrado agendamento com o cpf " + campos.cpf + " e com datahora " + campos.dataHora);
                            return;
                        }

                    })
                    .catch(function (error) {
                        reject("Erro ao excluir agendamento." + error);
                        return;
                    });
            }

        });

    }


    verificaSeExistePaciente(req, res) {
        return new Promise((resolve, reject) => {
            let cpf = req.body.cpf;
            let dataHora = req.body.dataHora;
            if (cpf) {

                bd.collection("agendamentos").where("cpf", "==", cpf).where("dataHora", "==", dataHora).get()
                    .then(snapshot => {

                        let dados = snapshot.docs.map(doc => doc.data());
                        if (dados.length >= numAgendamentosRepetidos) {
                            reject("Já existe um agendamento com este cpf e esta data.");
                            return;
                        }
                        else {
                            resolve();
                        }

                    })
                    .catch(function (error) {
                        reject("Erro ao verificar agendamento.");
                        return;
                    });
            }
            else {
                reject("Erro ao verificar agendamento. cpf em branco.");
                return;
            }
        })
    }


}

module.exports = agendamento;