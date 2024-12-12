FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PersonnelManagement.Api/PersonnelManagement.Api.csproj", "PersonnelManagement.Api/"]
RUN dotnet restore "PersonnelManagement.Api/PersonnelManagement.Api.csproj"
COPY . .
WORKDIR "/src/PersonnelManagement.Api"
RUN dotnet build "PersonnelManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PersonnelManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonnelManagement.Api.dll"]
