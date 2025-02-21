# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Instala Node.js dentro del contenedor
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - \
    && apt-get install -y nodejs

# Copiar archivos de la solución y restaurar dependencias
COPY *.sln ./
COPY SimpleService.Api/*.csproj SimpleService.Api/
COPY SimpleService.Application/*.csproj SimpleService.Application/
COPY SimpleService.Contracts/*.csproj SimpleService.Contracts/
COPY SimpleService.Infrastructure/*.csproj SimpleService.Infrastructure/
COPY SimpleService.Domain/*.csproj SimpleService.Domain/
RUN dotnet restore

# Copiar todo el código fuente
COPY . .

# Compilar y publicar la aplicación principal (API)
RUN dotnet publish SimpleService.Api/SimpleService.Api.csproj -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Configurar variables de entorno y expone el puerto 8080
ENV ASPNETCORE_URLS=http://+:5051
EXPOSE 8080

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "SimpleService.Api.dll"]