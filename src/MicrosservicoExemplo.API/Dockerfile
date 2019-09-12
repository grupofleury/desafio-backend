FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Fleury.Agendamento.API/Fleury.Agendamento.API.csproj", "src/Fleury.Agendamento.API/"]
RUN dotnet restore "src/MicrosservicoExemplo.API/MicrosservicoExemplo.API.csproj"
COPY . .
WORKDIR "/src/src/Fleury.Agendamento.API"
RUN dotnet build "Fleury.Agendamento.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Fleury.Agendamento.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fleury.Agendamento.API.dll"]