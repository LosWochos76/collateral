FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ToDo.Common ToDo.Common
COPY ToDo.Persistence ToDo.Persistence 
COPY ToDo.Persistence.EfCore ToDo.Persistence.EfCore 
COPY ToDo.Persistence.Dapper ToDo.Persistence.Dapper 
COPY ToDo.CQRS ToDo.CQRS
COPY ToDo.WebUi ToDo.WebUi
RUN dotnet restore ToDo.WebUi/ToDo.WebUi.csproj

# Copy everything else and build
RUN dotnet publish -c Release -o out ./ToDo.WebUi/ToDo.WebUi.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ToDo.WebUi.dll"]