FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY ClientsApi/*.csproj ./ClientsApi/
RUN dotnet restore ClientsApi/ClientsApi.csproj

COPY ClientsApi/. ./ClientsApi/
WORKDIR /app/ClientsApi
RUN dotnet publish ClientsApi.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/ClientsApi/out ./
ENTRYPOINT ["dotnet", "ClientsApi.dll"]

