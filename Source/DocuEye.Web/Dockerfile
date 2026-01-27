#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:10.0.2 AS base
WORKDIR /app
EXPOSE 8080

FROM node:20.19.6 as client-build
WORKDIR /node
COPY ./Source/DocuEye.Web/client-app /node
RUN npm install
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:10.0.102 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/DocuEye.Web/DocuEye.Web.csproj", "Source/DocuEye.Web/"]
RUN dotnet restore "./Source/DocuEye.Web/./DocuEye.Web.csproj"
COPY . .
WORKDIR "/src/Source/DocuEye.Web"
RUN dotnet build "./DocuEye.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DocuEye.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=client-build /node/build/client ./wwwroot
ENTRYPOINT ["dotnet", "DocuEye.Web.dll"]