FROM mcr.microsoft.com/mssql/server:2019-latest AS build
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@55w0rd123

WORKDIR /tmp
COPY create_db.sql .

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \    
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "P@55w0rd123" -i '/tmp/create_db.sql' \
    && pkill sqlservr

FROM mcr.microsoft.com/mssql/server:2019-latest AS final
ENV ACCEPT_EULA=Y

COPY --from=build /var/opt/mssql/data /var/opt/mssql/data





