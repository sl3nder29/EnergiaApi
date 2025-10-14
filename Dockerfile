# Use a imagem base mais leve para produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o arquivo de projeto primeiro para aproveitar cache do Docker
COPY ["EnergiaApi.csproj", "./"]
RUN dotnet restore "EnergiaApi.csproj"

# Copia o resto do código
COPY . .
WORKDIR "/src"

# Build da aplicação
RUN dotnet build "EnergiaApi.csproj" -c Release -o /app/build

# Estágio de publicação
FROM build AS publish
RUN dotnet publish "EnergiaApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio final de produção
FROM base AS final
WORKDIR /app

# Cria um usuário não-root para segurança
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Copia os arquivos publicados
COPY --from=publish /app/publish .

# Configura variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "EnergiaApi.dll"] 