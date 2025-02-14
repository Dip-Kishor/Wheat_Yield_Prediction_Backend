FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BackendCodeForML.Web/BackendCodeForML.Web.csproj", "BackendCodeForML.Web/"]
COPY ["BackendCodeForML.Data/BackendCodeForML.Data.csproj", "BackendCodeForML.Data/"]
COPY ["BackendCodeForML.Models/BackendCodeForML.Models.csproj", "BackendCodeForML.Models/"]
COPY ["BackendCodeForML.Services/BackendCodeForML.Services.csproj", "BackendCodeForML.Services/"]
COPY ["CommonServices/CommonServices.csproj", "CommonServices/"]
RUN dotnet restore "./BackendCodeForML.Web/BackendCodeForML.Web.csproj"
COPY . .
WORKDIR "/src/BackendCodeForML.Web"
RUN dotnet build "./BackendCodeForML.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BackendCodeForML.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendCodeForML.Web.dll"]