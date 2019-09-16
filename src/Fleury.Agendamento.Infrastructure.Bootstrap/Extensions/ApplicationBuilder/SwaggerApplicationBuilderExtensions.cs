using Microsoft.AspNetCore.Builder;

namespace Fleury.Agendamento.Infrastructure.Bootstrap.Extensions.ApplicationBuilder
{
    public static class SwaggerApplicationBuilderExtensions
    {
        public static void UseAgendamentoSwagger(this IApplicationBuilder app)
        {
            //Quando desenvolver multiplos microsservicos, o ideal é que cada serviço
            //apenas exponha uma rota com seu JSON de documentação e um hub do SwaggerUi
            //agregue todas as APIS. 
            // Para este cenário, utilize o código de exemplo abaixo:

            //app.UseSwagger(c =>
            //{
            //    c.RouteTemplate = "docs/{documentName}/docs.json";
            //});

            //Nesta sample, além da rota com o documento estamos também adicionando a UI do swagger
            //para possibilitar a visualização. Não a adicione no caso de microsserviços.
            //Use o exemplo acima!!!
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agendamento Fleury");
            });

        }
    }
}
