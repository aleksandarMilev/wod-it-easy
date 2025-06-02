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
COPY ["Profile/Profile.Application/Profile.Application.csproj", "Profile/Profile.Application/"]
COPY ["Profile/Profile.Infrastructure/Profile.Infrastructure.csproj", "Profile/Profile.Infrastructure/"]
COPY ["Profile/Profile.StartUp/Profile.StartUp.csproj", "Profile/Profile.StartUp/"]
COPY ["Profile/Profile.Web/Profile.Web.csproj", "Profile/Profile.Web/"]

RUN dotnet restore "Profile/Profile.StartUp/Profile.StartUp.csproj"

COPY . .

WORKDIR "/src/Profile/Profile.StartUp"
RUN dotnet build "Profile.StartUp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profile.StartUp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile.StartUp.dll"]
