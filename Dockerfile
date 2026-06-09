# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

COPY BankingAccounts.slnx ./
COPY BankingAccounts.Api/BankingAccounts.Api.csproj BankingAccounts.Api/
COPY BankingAccounts.Application/BankingAccounts.Application.csproj BankingAccounts.Application/
COPY BankingAccounts.Data/BankingAccounts.Data.csproj BankingAccounts.Data/
COPY BankingAccounts.Domain/BankingAccounts.Domain.csproj BankingAccounts.Domain/
COPY BankingAccounts.Infrastructure/BankingAccounts.Infrastructure.csproj BankingAccounts.Infrastructure/

RUN dotnet restore BankingAccounts.Api/BankingAccounts.Api.csproj

COPY . .

RUN dotnet publish BankingAccounts.Api/BankingAccounts.Api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish \
    /p:UseAppHost=false

FROM runtime AS final
RUN addgroup -S appgroup && adduser -S appuser -G appgroup
COPY --from=build /app/publish .
RUN mkdir -p /app/logs && chown -R appuser:appgroup /app
USER appuser
ENTRYPOINT ["dotnet", "BankingAccounts.Api.dll"]
