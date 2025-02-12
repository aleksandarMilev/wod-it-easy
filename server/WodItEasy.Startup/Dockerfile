FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 8081

RUN useradd -m appuser
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["server/WodItEasy.Startup/WodItEasy.Startup.csproj", "server/WodItEasy.Startup/"]
COPY ["server/WodItEasy.Infrastructure/WodItEasy.Infrastructure.csproj", "server/WodItEasy.Infrastructure/"]
COPY ["server/WodItEasy.Application/WodItEasy.Application.csproj", "server/WodItEasy.Application/"]
COPY ["server/WodItEasy.Domain/WodItEasy.Domain.csproj", "server/WodItEasy.Domain/"]
COPY ["server/WodItEasy.Web/WodItEasy.Web.csproj", "server/WodItEasy.Web/"]

RUN dotnet restore "server/WodItEasy.Startup/WodItEasy.Startup.csproj"

COPY server/ server/

WORKDIR "/src/server/WodItEasy.Startup"
RUN dotnet build "WodItEasy.Startup.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "WodItEasy.Startup.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WodItEasy.Startup.dll"]
