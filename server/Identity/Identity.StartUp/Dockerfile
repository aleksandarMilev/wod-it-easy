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
COPY ["Identity/Identity.Application/Identity.Application.csproj", "Identity/Identity.Application/"]
COPY ["Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "Identity/Identity.Infrastructure/"]
COPY ["Identity/Identity.StartUp/Identity.StartUp.csproj", "Identity/Identity.StartUp/"]
COPY ["Identity/Identity.Web/Identity.Web.csproj", "Identity/Identity.Web/"]

RUN dotnet restore "Identity/Identity.StartUp/Identity.StartUp.csproj"

COPY . .

WORKDIR "/src/Identity/Identity.StartUp"
RUN dotnet build "Identity.StartUp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.StartUp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.StartUp.dll"]
