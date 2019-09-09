FROM microsoft/dotnet:2.2-sdk
ARG debug='1'
ENV DEBUG ${debug}
WORKDIR /src
COPY . .
RUN dotnet restore --ignore-failed-sources || :
RUN dotnet restore --ignore-failed-sources --source http://10.33.1.34:8081/repository/nuget-group/ || :
RUN (test $DEBUG -eq 1 && dotnet publish -c Debug -o /app) || (test $DEBUG -eq 0 && dotnet publish -c Release -o /app)

FROM microsoft/dotnet:2.2-aspnetcore-runtime
RUN apt update && apt install -y telnet vim tcpdump iputils-ping
ARG deploy_env='dev'
ARG port='8080'
ARG debug='1'
ENV DEBUG ${debug}
RUN (test $DEBUG -eq 1 && apt update && apt install -y unzip procps && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l ~/vsdbg) || :
ARG deploy_env
ENV ASPNETCORE_ENVIRONMENT ${deploy_env}
ENV ASPNETCORE_URLS http://0.0.0.0:${port}
ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
COPY --from=0 /app .
CMD ["/bin/bash", "-c", "dotnet Exam.UI.dll"]
EXPOSE ${port}
