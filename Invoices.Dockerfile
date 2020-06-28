FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY InvoicesApi/*.csproj ./InvoicesApi/
RUN dotnet restore InvoicesApi/InvoicesApi.csproj

COPY InvoicesApi/. ./InvoicesApi/
WORKDIR /app/InvoicesApi
RUN dotnet publish InvoicesApi.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/InvoicesApi/out ./
ENTRYPOINT ["dotnet", "InvoicesApi.dll"]

