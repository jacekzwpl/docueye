#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:10.0.2 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0.102 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/DocuEye.CLI/DocuEye.CLI.csproj", "Source/DocuEye.CLI/"]
RUN dotnet restore "./Source/DocuEye.CLI/./DocuEye.CLI.csproj"
COPY . .
WORKDIR "/src/Source/DocuEye.CLI"
RUN dotnet build "./DocuEye.CLI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DocuEye.CLI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocuEye.CLI.dll"]