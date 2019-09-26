
class exame {

    listar(req, res) {
        return new Promise((resolve, reject) => {
        
            const objRequest = {
                uri: "http://www.mocky.io/v2/5d681ede33000054e7e65c3f",
                method: "POST"
            };
            request(objRequest, function (error, response) {
                if (error) {
                    reject();
                } else if (response.statusCode != 200) {
                    reject();
                } else {

                    let listaExames = [];
                    let jsonRetornado = JSON.parse(response.body);

                    jsonRetornado.exams.forEach(element => {
                        let exame = {
                            "id": element.id,
                            "nome": element.name
                        }

                        listaExames.push(exame);
                    });

                    resolve( { "Exames": listaExames } );
                }
            });


        });
    }
    
   
}

module.exports = exame;