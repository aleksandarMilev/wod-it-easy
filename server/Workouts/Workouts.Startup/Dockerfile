FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Common/Common.Application/Common.Application.csproj", "Common/Common.Application/"]
COPY ["Common/Common.Domain/Common.Domain.csproj", "Common/Common.Domain/"]
COPY ["Common/Common.Infrastructure/Common.Infrastructure.csproj", "Common/Common.Infrastructure/"]
COPY ["Common/Common.Web/Common.Web.csproj", "Common/Common.Web/"]
COPY ["Workouts/Workouts.Application/Workouts.Application.csproj", "Workouts/Workouts.Application/"]
COPY ["Workouts/Workouts.Infrastructure/Workouts.Infrastructure.csproj", "Workouts/Workouts.Infrastructure/"]
COPY ["Workouts/Workouts.StartUp/Workouts.StartUp.csproj", "Workouts/Workouts.StartUp/"]
COPY ["Workouts/Workouts.Web/Workouts.Web.csproj", "Workouts/Workouts.Web/"]

RUN dotnet restore "Workouts/Workouts.StartUp/Workouts.StartUp.csproj"

COPY . .

WORKDIR "/src/Workouts/Workouts.StartUp"
RUN dotnet build "Workouts.StartUp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workouts.StartUp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workouts.StartUp.dll"]
