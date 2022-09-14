FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY . ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV IMDbServer_Connection="Put your connection string"
ENV JWT_DECRYPT="Put your JWT Secret Key"

ENTRYPOINT ["dotnet", "IMDbServer.Api.dll"]