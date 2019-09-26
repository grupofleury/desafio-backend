var utilClass = require("../classes/utils")
var utils = new utilClass();

class paciente {

    listar(req, res) {
        return new Promise((resolve, reject) => {

            bd.collection("pacientes").get()
                .then(snapshot => {
                    let dados = snapshot.docs.map(
                        doc => doc.data()
                    );
                    resolve(dados);
                })
                .catch(erro => {
                    reject('Erro ao retornar pacientes.' + error)
                });
        })

    }

    consultaPorCPF(req) {
        return new Promise((resolve, reject) => {

            if (req.body.cpf) {
                let cpf = req.body.cpf;

                bd.collection("pacientes").where("cpf", "==", cpf).get()
                    .then(snapshot => {
                        let dados = snapshot.docs.map(doc => doc.data());

                        if (dados.length > 0) {
                            resolve(dados[0]);
                        }
                        else {
                            reject("Não foi encontrado paciente com o cpf " + cpf);
                        }

                    })
                    .catch(erro => {
                        reject('Erro ao retornar paciente.' + erro)
                    });
            }

        })

    }

    inserir(req, res) {
        return new Promise((resolve, reject) => {
            let campos = {};
            let erros = [];

            if (req.body.nome) {
                campos.nome = req.body.nome;
            }
            else
                erros.push("nome");

            if (req.body.dataNascimento) {
                campos.dataNascimento = req.body.dataNascimento;
            }
            else
                erros.push("dataNascimento");

            if (req.body.cpf) {
                campos.cpf = req.body.cpf;
            }
            else
                erros.push("cpf");

            if (!utils.ValidaCPF(campos.cpf)) {
                reject('Erro ao inserir paciente. CPF inválido.');
                return;
            }

            campos.ativo = "true";
            campos.dataCriacao = firebase.firestore.FieldValue.serverTimestamp();
            campos.dataAlteracao = firebase.firestore.FieldValue.serverTimestamp();

            if (erros.length > 0) {
                reject('Erro ao inserir paciente. Campo ' + erros[0] + ' é obrigatório.');
                return;
            }

            this.verificaSeExiste(req, res)
                .then(existe => {

                    bd.collection("pacientes").add(campos)
                        .then(snapshot => {
                            resolve();
                        })
                        .catch(function (error) {
                            reject('Erro ao inserir paciente. ' + error);
                            return;
                        });

                })
                .catch(function (error) {
                    reject('Erro ao inserir paciente. ' + error);
                    return;
                });
               
        })
        
    }

    alterar(req, res) {
        return new Promise((resolve, reject) => {
            let campos = {};
            let erros = [];

            if (req.body.nome) {
                campos.nome = req.body.nome;
            }
            else
                erros.push("nome");

            if (req.body.dataNascimento) {
                campos.dataNascimento = req.body.dataNascimento;
            }
            else
                erros.push("dataNascimento");

            if (req.body.cpf) {

                if (!utils.ValidaCPF(req.body.cpf)) {
                    reject("Erro ao alterar paciente. CPF inválido.");
                    return;
                }
            }
            else
                erros.push("cpf");

            if (req.body.ativo) {
                campos.ativo = req.body.ativo;
            }

            campos.dataAlteracao = firebase.firestore.FieldValue.serverTimestamp();

            if (erros.length > 0) {
                reject("Erro ao alterar paciente. Campo " + erros[0] + " é obrigatório.");
                return;
            }

            if (req.body.cpf && req.body.cpf != "" && req.body.cpf != 0) {
                bd.collection("pacientes").where("cpf", "==", req.body.cpf).get()
                    .then(snapshot => {

                        let dados = snapshot.docs.map(doc => doc.data());
                        if (dados.length > 0) {

                            let id = snapshot.docs[0].id;
                            bd.collection("pacientes").doc(id).update(campos)
                                .then(snapshot => {
                                    resolve()
                                })
                                .catch(function (error) {
                                    reject("Erro ao alterar paciente. " + error);
                                    return;
                                });

                        }
                        else {
                            reject("Não foi encontrado paciente com o cpf " + req.body.cpf);
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
            }
            else
                erros.push("cpf");

            if (!utils.ValidaCPF(campos.cpf)) {
                reject("Erro ao excluir paciente. CPF inválido.");
                return;
            }

            if (erros.length > 0) {
                reject("Erro ao excluir paciente. Campo " + erros[0] + " é obrigatório.");
                return;
            }

            if (campos.cpf && campos.cpf != "" && campos.cpf != 0) {
                bd.collection("pacientes").where("cpf", "==", campos.cpf).get()
                    .then(snapshot => {

                        let dados = snapshot.docs.map(doc => doc.data());
                        if (dados.length > 0) {

                            let id = snapshot.docs[0].id;
                            bd.collection("pacientes").doc(id).delete();
                            resolve();
                        }
                        else {
                            reject("Não foi encontrado paciente com o cpf " + campos.cpf);
                            return;
                        }

                    })
                    .catch(function (error) {
                        reject("Erro ao excluir paciente." + error);
                        return;
                    });
            }

        });

    }

    verificaSeExiste(req, res) {
        return new Promise((resolve, reject) => {
            let cpf = req.body.cpf;
            if (cpf) {

                bd.collection("pacientes").where("cpf", "==", cpf).get()
                    .then(snapshot => {

                        let dados = snapshot.docs.map(doc => doc.data());
                        if (dados.length == 0) {
                            resolve()
                        }
                        else {
                            if (req.url == "/paciente/inserir") {
                                reject("Já existe um paciente com o cpf " + cpf);
                            }
                        }

                    })
                    .catch(function (error) {
                        reject("Erro ao alterar paciente.");
                    });
            }
            else {
                reject("Erro ao inserir paciente. cpf em branco.");
            }
        })
    }

}

module.exports = paciente;