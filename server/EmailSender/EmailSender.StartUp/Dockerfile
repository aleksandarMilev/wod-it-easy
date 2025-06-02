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
COPY ["EmailSender/EmailSender.Application/EmailSender.Application.csproj", "EmailSender/EmailSender.Application/"]
COPY ["EmailSender/EmailSender.Infrastructure/EmailSender.Infrastructure.csproj", "EmailSender/EmailSender.Infrastructure/"]
COPY ["EmailSender/EmailSender.StartUp/EmailSender.StartUp.csproj", "EmailSender/EmailSender.StartUp/"]
COPY ["EmailSender/EmailSender.Web/EmailSender.Web.csproj", "EmailSender/EmailSender.Web/"]

RUN dotnet restore "EmailSender/EmailSender.StartUp/EmailSender.StartUp.csproj"

COPY . .

WORKDIR "/src/EmailSender/EmailSender.StartUp"
RUN dotnet build "EmailSender.StartUp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailSender.StartUp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailSender.StartUp.dll"]
