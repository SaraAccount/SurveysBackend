FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# העתק רק את קובץ הסולושן
COPY Web_Api/Web_Api.sln ./

# העתק את כל תיקיות הפרויקטים לפי המבנה שלך
COPY Web_Api/Web_API/Web_API.csproj Web_API/
COPY Web_Api/Repository/Repository.csproj Repository/
COPY Web_Api/Service/Service.csproj Service/
COPY Web_Api/Common/Common.csproj Common/

# הרצת restore
RUN dotnet restore

# עכשיו העתק את כל הקוד
COPY Web_Api/ ./

# פרסום
RUN dotnet publish Web_API/Web_API.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Web_API.dll"]
