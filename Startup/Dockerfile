#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Startup/Startup.csproj", "Startup/"]
COPY ["Api/Api.csproj", "Api/"]
COPY ["Interfaces.Services/Interfaces.Services.csproj", "Interfaces.Services/"]
COPY ["Interfaces.Common/Interfaces.Common.csproj", "Interfaces.Common/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Interfaces.Data/Interfaces.Data.csproj", "Interfaces.Data/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "./Startup/Startup.csproj"
COPY . .
WORKDIR "/src/Startup"
RUN dotnet build "./Startup.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Startup.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Startup.dll"]