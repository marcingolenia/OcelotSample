FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY ApiGateway/*.csproj ./ApiGateway/
RUN dotnet restore ApiGateway/ApiGateway.csproj

COPY ApiGateway/. ./ApiGateway/
WORKDIR /app/ApiGateway
RUN dotnet publish ApiGateway.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/ApiGateway/out ./
ENTRYPOINT ["dotnet", "ApiGateway.dll"]

