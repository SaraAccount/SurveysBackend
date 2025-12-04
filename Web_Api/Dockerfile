# שלב 1: בנייה (Build)
# משתמשים בתמונת SDK המלאה כדי לבנות את הקוד
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# העתקת קובץ ה-Solution וקובצי ה-Project בלבד (לצורך ניצול Cache של דוקר)
COPY ["Web_Api.sln", "./"]
COPY ["Web_API/Web_API.csproj", "Web_API/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Mock/Mock.csproj", "Mock/"]

# שחזור החבילות (Restore)
RUN dotnet restore "Web_Api.sln"

# העתקת שאר הקבצים
COPY . .

# בניית הפרויקט
WORKDIR "/src/Web_API"
RUN dotnet build "Web_API.csproj" -c Release -o /app/build

# פרסום הפרויקט (Publish) - יצירת קבצי הרצה
FROM build AS publish
RUN dotnet publish "Web_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# שלב 2: הרצה (Runtime)
# משתמשים בתמונה קלה יותר שרק מריצה את האפליקציה
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web_API.dll"]