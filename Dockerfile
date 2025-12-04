FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app


COPY Web_Api/Web_Api.sln ./

COPY Web_Api/*.csproj ./Web_API/

RUN dotnet restore

COPY Web_Api/ ./

RUN dotnet publish -c Release -o out
