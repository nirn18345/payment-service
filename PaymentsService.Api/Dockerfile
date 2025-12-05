# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PaymentsService.Api/PaymentsService.Api.csproj", "PaymentsService.Api/"]
COPY ["PaymentsService.Application/PaymentsService.Application.csproj", "PaymentsService.Application/"]
COPY ["PaymentsService.Domain/PaymentsService.Domain.csproj", "PaymentsService.Domain/"]
COPY ["PaymentsService.Infraestructure/PaymentsService.Infraestructure.csproj", "PaymentsService.Infraestructure/"]

RUN dotnet restore "PaymentsService.Api/PaymentsService.Api.csproj"

COPY . .
WORKDIR "/src/PaymentsService.Api"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentsService.Api.dll"]
